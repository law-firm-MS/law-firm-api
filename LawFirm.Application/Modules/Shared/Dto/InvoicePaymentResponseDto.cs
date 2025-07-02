namespace LawFirm.Application.Modules.Shared.Dto
{
    public class InvoicePaymentResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? TransactionId { get; set; }
        public int InvoiceId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
