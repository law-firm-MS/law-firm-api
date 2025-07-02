using LawFirm.Domain.Modules.Appointments;
using LawFirm.Domain.Modules.AuditLogs;
using LawFirm.Domain.Modules.Cases;
using LawFirm.Domain.Modules.Clients;
using LawFirm.Domain.Modules.Documents;
using LawFirm.Domain.Modules.Expenses;
using LawFirm.Domain.Modules.Invoices;
using LawFirm.Domain.Modules.OAuth;
using LawFirm.Domain.Modules.Organizations;
using LawFirm.Domain.Modules.Shared;
using LawFirm.Domain.Modules.Tasks;
using LawFirm.Domain.Modules.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure
{
    public class LawFirmDbContext : IdentityDbContext<ApplicationUser>
    {
        public LawFirmDbContext(DbContextOptions<LawFirmDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<CaseUpdate> CaseUpdates { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<OAuthToken> OAuthTokens { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Organization> Organizations { get; set; }
    }
}
