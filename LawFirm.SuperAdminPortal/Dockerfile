# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY LawFirm.SuperAdminPortal/LawFirm.SuperAdminPortal.csproj ./
COPY LawFirm.Application/LawFirm.Application.csproj ../LawFirm.Application/
COPY LawFirm.Domain/LawFirm.Domain.csproj ../LawFirm.Domain/
RUN dotnet restore
COPY LawFirm.SuperAdminPortal/. .
COPY LawFirm.Application ../LawFirm.Application
COPY LawFirm.Domain ../LawFirm.Domain
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "LawFirm.SuperAdminPortal.dll"] 