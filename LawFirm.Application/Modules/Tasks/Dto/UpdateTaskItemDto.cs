using System;

namespace LawFirm.Application.Modules.Tasks.Dto
{
    public class UpdateTaskItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AssignedToUserId { get; set; } = string.Empty;
        public int? CaseId { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
