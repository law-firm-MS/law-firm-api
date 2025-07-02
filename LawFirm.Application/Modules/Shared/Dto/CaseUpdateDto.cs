using System;

namespace LawFirm.Application.Modules.Shared.Dto
{
    public class CaseUpdateDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CaseId { get; set; }
    }
}
