using LawFirm.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("api/reporting")]
    [Authorize(Roles = "Admin,Lawyer")]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingService _service;

        public ReportingController(IReportingService service)
        {
            _service = service;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var result = await _service.GetDashboardStatsAsync();
            return Ok(result);
        }

        [HttpGet("revenue-by-month")]
        public async Task<IActionResult> GetRevenueByMonth([FromQuery] int year)
        {
            var result = await _service.GetRevenueByMonthAsync(year);
            return Ok(result);
        }

        [HttpGet("case-status-breakdown")]
        public async Task<IActionResult> GetCaseStatusBreakdown()
        {
            var result = await _service.GetCaseStatusBreakdownAsync();
            return Ok(result);
        }
    }
}
