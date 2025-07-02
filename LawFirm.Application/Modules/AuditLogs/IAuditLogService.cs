using System.Collections.Generic;
using System.Threading.Tasks;
using LawFirm.Application.Modules.AuditLogs.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.AuditLogs;

namespace LawFirm.Application.Modules.AuditLogs
{
    public interface IAuditLogService
    {
        Task LogAsync(
            string userId,
            string action,
            string entity,
            string? entityId = null,
            string? details = null
        );

        Task<PagedResultDto<AuditLog>> QueryAsync(AuditLogQueryDto query);
    }
}
