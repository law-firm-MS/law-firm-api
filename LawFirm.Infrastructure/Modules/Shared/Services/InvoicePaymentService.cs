using System.Threading.Tasks;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.AuditLogs;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Invoices;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class InvoicePaymentService : IInvoicePaymentService
    {
        private readonly LawFirmDbContext _context;
        private readonly IPaymentGatewayService _paymentGateway;
        private readonly INotificationService _notificationService;
        private readonly IAuditLogService _auditLogService;

        public InvoicePaymentService(
            LawFirmDbContext context,
            IPaymentGatewayService paymentGateway,
            INotificationService notificationService,
            IAuditLogService auditLogService
        )
        {
            _context = context;
            _paymentGateway = paymentGateway;
            _notificationService = notificationService;
            _auditLogService = auditLogService;
        }

        public async Task<InvoicePaymentResponseDto> PayInvoiceAsync(
            int invoiceId,
            InvoicePaymentRequestDto dto,
            string clientEmail
        )
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
            {
                return new InvoicePaymentResponseDto
                {
                    Success = false,
                    Message = "Invoice not found.",
                };
            }
            if (invoice.Status == InvoiceStatus.Paid)
            {
                return new InvoicePaymentResponseDto
                {
                    Success = false,
                    Message = "Invoice already paid.",
                };
            }
            var paymentSuccess = await _paymentGateway.ProcessPaymentAsync(
                dto.InvoiceId,
                dto.Amount,
                dto.PaymentMethod,
                clientEmail
            );
            if (!paymentSuccess)
            {
                return new InvoicePaymentResponseDto
                {
                    Success = false,
                    Message = "Payment failed.",
                };
            }
            invoice.Status = InvoiceStatus.Paid;
            await _context.SaveChangesAsync();
            // Send notification (mock)
            if (!string.IsNullOrEmpty(clientEmail))
            {
                await _notificationService.SendEmailAsync(
                    clientEmail,
                    "Invoice Paid",
                    $"Your payment for invoice #{invoice.InvoiceNumber} was successful."
                );
            }
            // Audit log
            await _auditLogService.LogAsync(
                dto.UserId ?? "anonymous",
                "PayInvoice",
                "Invoice",
                invoice.Id.ToString(),
                $"Amount: {dto.Amount}, Method: {dto.PaymentMethod}"
            );
            return new InvoicePaymentResponseDto
            {
                Success = true,
                Message = "Payment successful.",
            };
        }
    }
}
