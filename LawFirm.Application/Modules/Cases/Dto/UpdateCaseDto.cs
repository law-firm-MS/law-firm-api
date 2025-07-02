using System;

namespace LawFirm.Application.Modules.Cases.Dto
{
    public class UpdateCaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime OpenDate { get; set; }
        public int ClientId { get; set; }
    }
}
