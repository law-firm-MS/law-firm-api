using System.Threading.Tasks;
using LawFirm.Application.Modules.AuditLogs;
using LawFirm.Application.Modules.AuditLogs.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.AuditLogs;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly LawFirmDbContext _context;

        public AuditLogService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(
            string userId,
            string action,
            string entity,
            string? entityId = null,
            string? details = null
        )
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Entity = entity,
                EntityId = entityId,
                Details = details,
            };
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResultDto<AuditLog>> QueryAsync(AuditLogQueryDto query)
        {
            var logs = _context.AuditLogs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.UserId))
                logs = logs.Where(l => l.UserId == query.UserId);
            if (!string.IsNullOrWhiteSpace(query.Action))
                logs = logs.Where(l => l.Action == query.Action);
            if (!string.IsNullOrWhiteSpace(query.Entity))
                logs = logs.Where(l => l.Entity == query.Entity);
            if (!string.IsNullOrWhiteSpace(query.EntityId))
                logs = logs.Where(l => l.EntityId == query.EntityId);
            if (query.From.HasValue)
                logs = logs.Where(l => l.Timestamp >= query.From);
            if (query.To.HasValue)
                logs = logs.Where(l => l.Timestamp <= query.To);
            var totalCount = await logs.CountAsync();
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = query.PageSize < 1 ? 20 : query.PageSize;
            var items = await logs.OrderByDescending(l => l.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResultDto<AuditLog>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
            };
        }
    }
}
