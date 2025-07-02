using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Reporting.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Invoices;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class ReportingService : IReportingService
    {
        private readonly LawFirmDbContext _context;

        public ReportingService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var now = DateTime.UtcNow;
            var overdueInvoices = await _context.Invoices.CountAsync(i =>
                i.Status != InvoiceStatus.Paid && i.DueDate < now
            );
            return new DashboardStatsDto
            {
                TotalClients = await _context.Clients.CountAsync(),
                TotalCases = await _context.Cases.CountAsync(),
                TotalInvoices = await _context.Invoices.CountAsync(),
                TotalRevenue =
                    await _context
                        .Invoices.Where(i => i.Status == InvoiceStatus.Paid)
                        .SumAsync(i => (decimal?)i.Amount) ?? 0,
                OverdueInvoices = overdueInvoices,
            };
        }

        public async Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(int year)
        {
            return await _context
                .Invoices.Where(i => i.Status == InvoiceStatus.Paid && i.DueDate.Year == year)
                .GroupBy(i => new { i.DueDate.Year, i.DueDate.Month })
                .Select(g => new RevenueByMonthDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(i => i.Amount),
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();
        }

        public async Task<IEnumerable<CaseStatusBreakdownDto>> GetCaseStatusBreakdownAsync()
        {
            return await _context
                .Cases.GroupBy(c => c.Status)
                .Select(g => new CaseStatusBreakdownDto
                {
                    Status = g.Key.ToString(),
                    Count = g.Count(),
                })
                .ToListAsync();
        }
    }
}
