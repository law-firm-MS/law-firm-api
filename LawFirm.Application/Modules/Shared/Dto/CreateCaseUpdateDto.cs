using System;

namespace LawFirm.Application.Modules.Shared.Dto
{
    public class CreateCaseUpdateDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
