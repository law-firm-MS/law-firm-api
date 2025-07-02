using System.Threading.Tasks;
using LawFirm.Application.Interfaces;

namespace LawFirm.Infrastructure.Services
{
    public class MockPaymentGatewayService : IPaymentGatewayService
    {
        public Task<bool> ProcessPaymentAsync(
            int invoiceId,
            decimal amount,
            string paymentMethod,
            string? clientEmail = null
        )
        {
            // Simulate payment processing
            return Task.FromResult(true);
        }
    }
}
