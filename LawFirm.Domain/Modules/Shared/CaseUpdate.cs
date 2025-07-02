using System;

namespace LawFirm.Domain.Modules.Shared
{
    public class CaseUpdate
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string UpdateText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation property
        public Cases.Case? Case { get; set; }
    }
}
