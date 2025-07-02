using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Application.Modules.Tasks;
using LawFirm.Application.Modules.Tasks.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly LawFirmDbContext _context;

        public TaskItemService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto)
        {
            var entity = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                AssignedToUserId = dto.AssignedToUserId,
                CaseId = dto.CaseId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
            };
            _context.TaskItems.Add(entity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(entity.Id) ?? throw new Exception("Task creation failed");
        }

        public async Task<TaskItemDto?> GetByIdAsync(int id)
        {
            var t = await _context.TaskItems.FindAsync(id);
            if (t == null)
                return null;
            return new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Status = t.Status,
                AssignedToUserId = t.AssignedToUserId,
                CaseId = t.CaseId,
                CreatedAt = t.CreatedAt,
                CompletedAt = t.CompletedAt,
            };
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync(
            string? assignedToUserId = null,
            int? caseId = null,
            string? status = null
        )
        {
            var query = _context.TaskItems.AsQueryable();
            if (!string.IsNullOrEmpty(assignedToUserId))
                query = query.Where(t => t.AssignedToUserId == assignedToUserId);
            if (caseId.HasValue)
                query = query.Where(t => t.CaseId == caseId);
            if (!string.IsNullOrEmpty(status))
                query = query.Where(t => t.Status == status);
            return await query
                .OrderBy(t => t.DueDate)
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    AssignedToUserId = t.AssignedToUserId,
                    CaseId = t.CaseId,
                    CreatedAt = t.CreatedAt,
                    CompletedAt = t.CompletedAt,
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskItemDto dto)
        {
            var t = await _context.TaskItems.FindAsync(id);
            if (t == null)
                return false;
            t.Title = dto.Title;
            t.Description = dto.Description;
            t.DueDate = dto.DueDate;
            t.Status = dto.Status;
            t.AssignedToUserId = dto.AssignedToUserId;
            t.CaseId = dto.CaseId;
            t.CompletedAt = dto.CompletedAt;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var t = await _context.TaskItems.FindAsync(id);
            if (t == null)
                return false;
            _context.TaskItems.Remove(t);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
