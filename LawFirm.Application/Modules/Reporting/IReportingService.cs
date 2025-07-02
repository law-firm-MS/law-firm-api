using LawFirm.Application.Modules.Reporting.Dto;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IReportingService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
        Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(int year);
        Task<IEnumerable<CaseStatusBreakdownDto>> GetCaseStatusBreakdownAsync();
    }
}
