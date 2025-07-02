using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IInvoicePaymentService
    {
        Task<InvoicePaymentResponseDto> PayInvoiceAsync(
            int invoiceId,
            InvoicePaymentRequestDto dto,
            string clientEmail
        );
    }
}
