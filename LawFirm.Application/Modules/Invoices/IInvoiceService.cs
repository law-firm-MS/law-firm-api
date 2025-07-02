using LawFirm.Application.Modules.Invoices.Dto;

namespace LawFirm.Application.Modules.Invoices
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetByCaseIdAsync(int caseId);
        Task<InvoiceDto> CreateAsync(int caseId, CreateInvoiceDto dto);
    }
}
