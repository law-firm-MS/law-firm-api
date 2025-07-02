using System;

namespace LawFirm.Domain.Modules.Tasks
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, InProgress, Completed
        public string AssignedToUserId { get; set; } = string.Empty;
        public int? CaseId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}
