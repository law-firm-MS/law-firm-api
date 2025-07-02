namespace LawFirm.Application.Modules.Shared.Dto
{
    public class GlobalSearchResultDto
    {
        public string EntityType { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Snippet { get; set; }
    }
}
