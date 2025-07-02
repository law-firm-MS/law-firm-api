namespace LawFirm.Application.Interfaces
{
    public interface IPaymentGatewayService
    {
        Task<bool> ProcessPaymentAsync(
            int invoiceId,
            decimal amount,
            string paymentMethod,
            string? clientEmail = null
        );
    }
}
