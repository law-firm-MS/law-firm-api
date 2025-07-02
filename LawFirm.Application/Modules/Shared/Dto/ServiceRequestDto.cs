namespace LawFirm.Application.Modules.Shared.Dto
{
    public class ServiceRequestDto
    {
        public string ServiceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string>? Attachments { get; set; }
    }
}
