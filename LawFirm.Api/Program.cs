using System.Text;
using System.Threading;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.AuditLogs;
using LawFirm.Application.Modules.Cases;
using LawFirm.Application.Modules.Clients;
using LawFirm.Application.Modules.Documents;
using LawFirm.Application.Modules.Expenses;
using LawFirm.Application.Modules.Invoices;
using LawFirm.Application.Modules.Organizations;
using LawFirm.Application.Modules.Reporting;
using LawFirm.Application.Modules.Tasks;
using LawFirm.Domain.Modules.Users;
using LawFirm.Infrastructure;
using LawFirm.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure PostgreSQL DbContext
builder.Services.AddDbContext<LawFirmDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add Identity
builder
    .Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<LawFirmDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<IReportingService, ReportingService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IAdminInvoiceService, AdminInvoiceService>();
builder.Services.AddScoped<IPaymentGatewayService, MockPaymentGatewayService>();
builder.Services.AddScoped<IInvoicePaymentService, InvoicePaymentService>();
builder.Services.AddScoped<IGlobalSearchService, GlobalSearchService>();
builder.Services.AddScoped<INotificationService, SmtpEmailService>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LawFirm.Api.DistributedRateLimitingMiddleware>();
app.UseMiddleware<LawFirm.Api.RateLimitingMiddleware>();
app.UseMiddleware<LawFirm.Api.SecurityHeadersMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LawFirm.Api.TenantContextMiddleware>();
app.UseMiddleware<LawFirm.Api.GlobalExceptionMiddleware>();
app.UseHsts();

var supportedCultures = new[] { "en-US", "ar-SA" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en-US")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.MapControllers();

// Retry logic for migrations
void ApplyMigrationsWithRetry(IServiceProvider services)
{
    var maxAttempts = 10;
    var delay = TimeSpan.FromSeconds(5);
    var attempts = 0;
    while (true)
    {
        try
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LawFirmDbContext>();
                db.Database.Migrate();
            }
            break; // Success!
        }
        catch (NpgsqlException)
        {
            attempts++;
            if (attempts >= maxAttempts)
                throw;
            Thread.Sleep(delay);
        }
    }
}

// Apply pending migrations at startup (automatic)
ApplyMigrationsWithRetry(app.Services);

// Seed superadmin user
void SeedSuperAdminUser(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var superAdminEmail = "superadmin@lawfirm.local";
    var superAdminPassword = "SuperAdmin123!";
    var superAdminRole = "Admin";

    // Ensure role exists
    if (!roleManager.RoleExistsAsync(superAdminRole).Result)
    {
        roleManager.CreateAsync(new IdentityRole(superAdminRole)).Wait();
    }

    var user = userManager.FindByEmailAsync(superAdminEmail).Result;
    if (user == null)
    {
        var superAdmin = new ApplicationUser
        {
            UserName = superAdminEmail,
            Email = superAdminEmail,
            EmailConfirmed = true,
            FirstName = "Super",
            LastName = "Admin",
            Role = superAdminRole,
        };
        var result = userManager.CreateAsync(superAdmin, superAdminPassword).Result;
        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(superAdmin, superAdminRole).Wait();
        }
    }
}

SeedSuperAdminUser(app.Services);

// Support manual migration via --migrate argument
if (args.Contains("--migrate"))
{
    ApplyMigrationsWithRetry(app.Services);
    return;
}

app.Run();
