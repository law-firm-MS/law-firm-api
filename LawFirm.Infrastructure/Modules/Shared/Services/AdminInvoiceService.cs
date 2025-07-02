using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Invoices.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Cases;
using LawFirm.Domain.Modules.Invoices;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class AdminInvoiceService : IAdminInvoiceService
    {
        private readonly LawFirmDbContext _context;

        public AdminInvoiceService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<AdminInvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            var status = Enum.TryParse<InvoiceStatus>(dto.Status, out var parsedStatus)
                ? parsedStatus
                : InvoiceStatus.Draft;
            var invoice = new Invoice
            {
                InvoiceNumber = dto.InvoiceNumber,
                Amount = dto.Amount,
                DueDate = dto.DueDate,
                Status = status,
                CaseId = dto.CaseId,
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            var c = await _context.Cases.FindAsync(invoice.CaseId);
            return new AdminInvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Status = invoice.Status.ToString(),
                CaseId = invoice.CaseId,
                ClientId = c?.ClientId ?? 0,
            };
        }

        public async Task<PagedResultDto<AdminInvoiceDto>> GetAllInvoicesAsync(
            InvoiceQueryParametersDto query
        )
        {
            var invoices = _context.Invoices.Include(i => i.Case).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                invoices = invoices.Where(i =>
                    i.InvoiceNumber.Contains(query.Search)
                    || (
                        i.Case != null
                        && (
                            i.Case.CaseNumber.Contains(query.Search)
                            || i.Case.Client.Name.Contains(query.Search)
                        )
                    )
                );
            }
            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                if (Enum.TryParse<InvoiceStatus>(query.Status, out var status))
                    invoices = invoices.Where(i => i.Status == status);
            }
            if (query.ClientId.HasValue)
            {
                invoices = invoices.Where(i => i.Case != null && i.Case.ClientId == query.ClientId);
            }
            if (query.CaseId.HasValue)
            {
                invoices = invoices.Where(i => i.CaseId == query.CaseId);
            }
            if (query.DueDateFrom.HasValue)
            {
                invoices = invoices.Where(i => i.DueDate >= query.DueDateFrom);
            }
            if (query.DueDateTo.HasValue)
            {
                invoices = invoices.Where(i => i.DueDate <= query.DueDateTo);
            }

            var totalCount = await invoices.CountAsync();
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = query.PageSize < 1 ? 20 : query.PageSize;
            var items = await invoices
                .OrderByDescending(i => i.DueDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(i => new AdminInvoiceDto
                {
                    Id = i.Id,
                    InvoiceNumber = i.InvoiceNumber,
                    Amount = i.Amount,
                    DueDate = i.DueDate,
                    Status = i.Status.ToString(),
                    CaseId = i.CaseId,
                    ClientId = i.Case != null ? i.Case.ClientId : 0,
                })
                .ToListAsync();

            return new PagedResultDto<AdminInvoiceDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
            };
        }

        public async Task<AdminInvoiceDto?> GetInvoiceByIdAsync(int invoiceId)
        {
            var i = await _context
                .Invoices.Include(x => x.Case)
                .FirstOrDefaultAsync(x => x.Id == invoiceId);
            if (i == null)
                return null;
            return new AdminInvoiceDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                Amount = i.Amount,
                DueDate = i.DueDate,
                Status = i.Status.ToString(),
                CaseId = i.CaseId,
                ClientId = i.Case != null ? i.Case.ClientId : 0,
            };
        }

        public async Task<bool> UpdateInvoiceAsync(int invoiceId, UpdateInvoiceDto dto)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
                return false;
            invoice.Amount = dto.Amount;
            invoice.DueDate = dto.DueDate;
            invoice.Status = Enum.TryParse<InvoiceStatus>(dto.Status, out var parsedStatus)
                ? parsedStatus
                : invoice.Status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteInvoiceAsync(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
                return false;
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkInvoicePaidAsync(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
                return false;
            invoice.Status = InvoiceStatus.Paid;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
