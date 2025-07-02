using LawFirm.Application.Modules.Invoices.Dto;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IAdminInvoiceService
    {
        Task<AdminInvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<PagedResultDto<AdminInvoiceDto>> GetAllInvoicesAsync(InvoiceQueryParametersDto query);
        Task<AdminInvoiceDto?> GetInvoiceByIdAsync(int invoiceId);
        Task<bool> UpdateInvoiceAsync(int invoiceId, UpdateInvoiceDto dto);
        Task<bool> DeleteInvoiceAsync(int invoiceId);
        Task<bool> MarkInvoicePaidAsync(int invoiceId);
    }
}
