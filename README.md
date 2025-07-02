# Law Firm API & SuperAdmin Portal

## Overview

This project is a full-stack law firm management system, including:

- **LawFirm.Api**: ASP.NET Core Web API for law firm operations
- **LawFirm.SuperAdminPortal**: Blazor Server portal for superadmin management
- **PostgreSQL**: Database
- **Redis**: Caching
- **Mailhog**: Email testing

## Prerequisites

- Docker & Docker Compose
- .NET 8 SDK (for development)

## Quick Start (Docker Compose)

1. **Clone the repository**
2. **Build and run all services:**
   ```sh
   docker-compose up --build -d
   ```
3. **Access the services:**
   - API: [http://localhost:8080](http://localhost:8080)
   - SuperAdmin Portal: [http://localhost:5002](http://localhost:5002)
   - Mailhog: [http://localhost:8025](http://localhost:8025)

## SuperAdmin Login

A superadmin user is seeded automatically:

- **Email:** `superadmin@lawfirm.local`
- **Password:** `SuperAdmin123!`

## Development

- API code: `LawFirm.Api/`
- SuperAdmin Portal: `LawFirm.SuperAdminPortal/`
- Database migrations: `LawFirm.Infrastructure/Migrations/`

### Running Migrations

To add a migration:

```sh
cd LawFirm.Infrastructure
# Add migration
 dotnet ef migrations add <MigrationName> --startup-project ../LawFirm.Api --context LawFirmDbContext
# Apply migration
 dotnet ef database update --startup-project ../LawFirm.Api --context LawFirmDbContext
```

## Environment Variables

See `docker-compose.yml` for all environment variables and connection strings.

## Troubleshooting

- Ensure Docker containers are running: `docker ps`
- Check logs: `docker logs <container-name>`
- If migrations fail, check your connection strings and database status.

---

**Happy coding!**
