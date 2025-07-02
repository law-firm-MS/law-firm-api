using LawFirm.Application.Modules.Tasks.Dto;

namespace LawFirm.Application.Modules.Tasks
{
    public interface ITaskItemService
    {
        Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto);
        Task<TaskItemDto?> GetByIdAsync(int id);
        Task<IEnumerable<TaskItemDto>> GetAllAsync(
            string? assignedToUserId = null,
            int? caseId = null,
            string? status = null
        );
        Task<bool> UpdateAsync(int id, UpdateTaskItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
