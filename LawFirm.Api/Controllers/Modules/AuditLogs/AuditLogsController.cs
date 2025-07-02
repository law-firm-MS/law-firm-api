using LawFirm.Application.Modules.AuditLogs;
using LawFirm.Application.Modules.AuditLogs.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("audit-logs")]
    [Authorize(Roles = "Admin")]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _service;

        public AuditLogsController(IAuditLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Query([FromQuery] AuditLogQueryDto query)
        {
            var result = await _service.QueryAsync(query);
            return Ok(result);
        }
    }
}
