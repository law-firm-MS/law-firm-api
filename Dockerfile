# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY LawFirm.sln ./
COPY LawFirm.Domain/LawFirm.Domain.csproj LawFirm.Domain/
COPY LawFirm.Application/LawFirm.Application.csproj LawFirm.Application/
COPY LawFirm.Infrastructure/LawFirm.Infrastructure.csproj LawFirm.Infrastructure/
COPY LawFirm.Api/LawFirm.Api.csproj LawFirm.Api/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/LawFirm.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# Install curl for healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
COPY --from=build /app/publish .

# Expose port
EXPOSE 80

# Health check for Docker/K8s
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD curl --fail http://localhost/health || exit 1

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "LawFirm.Api.dll"] 