namespace LawFirm.Application.Modules.Cases.Dto
{
    public class CreateCaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public DateTime OpenDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CaseNumber { get; set; } = string.Empty;
    }
}
