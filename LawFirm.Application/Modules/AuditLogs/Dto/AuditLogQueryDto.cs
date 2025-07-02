namespace LawFirm.Application.Modules.AuditLogs.Dto
{
    public class AuditLogQueryDto
    {
        public string? UserId { get; set; }
        public string? Action { get; set; }
        public string? Entity { get; set; }
        public string? EntityId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
