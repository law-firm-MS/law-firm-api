namespace LawFirm.Application.Modules.Shared.Dto
{
    public class InvoicePaymentRequestDto
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int InvoiceId { get; set; }
        public string? UserId { get; set; }
        // Add more fields as needed (e.g., card info, token, etc.)
    }
}
