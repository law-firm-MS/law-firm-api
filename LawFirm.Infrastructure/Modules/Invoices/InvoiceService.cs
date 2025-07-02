using LawFirm.Application.Modules.Invoices;
using LawFirm.Application.Modules.Invoices.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain.Modules.Cases;
using LawFirm.Domain.Modules.Invoices;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly LawFirmDbContext _context;

        public InvoiceService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvoiceDto>> GetByCaseIdAsync(int caseId)
        {
            return await _context
                .Invoices.Where(i => i.CaseId == caseId)
                .Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    Amount = i.Amount,
                    DueDate = i.DueDate,
                    Status = i.Status.ToString(),
                    CaseId = i.CaseId,
                })
                .ToListAsync();
        }

        public async Task<InvoiceDto> CreateAsync(int caseId, CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                Amount = dto.Amount,
                DueDate = dto.DueDate,
                Status = InvoiceStatus.Open,
                CaseId = caseId,
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return new InvoiceDto
            {
                Id = invoice.Id,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Status = invoice.Status.ToString(),
                CaseId = invoice.CaseId,
            };
        }
    }
}
