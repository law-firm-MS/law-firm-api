namespace LawFirm.Application.Modules.Cases.Dto
{
    public class CaseQueryParametersDto
    {
        public string? Search { get; set; }
        public string? Status { get; set; }
        public int? ClientId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
