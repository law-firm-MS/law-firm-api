using LawFirm.Application.Modules.Cases;
using LawFirm.Application.Modules.Cases.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Cases;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class CaseService : ICaseService
    {
        private readonly LawFirmDbContext _context;

        public CaseService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CaseDto>> GetAllAsync()
        {
            return await _context
                .Cases.Select(c => new CaseDto
                {
                    Id = c.Id,
                    CaseNumber = c.CaseNumber,
                    Title = c.Title,
                    Description = c.Description,
                    Status = c.Status.ToString(),
                    OpenDate = c.OpenDate,
                    ClientId = c.ClientId,
                    InvoiceIds = c.Invoices.Select(i => i.Id).ToList(),
                    ExpenseIds = c.Expenses.Select(e => e.Id).ToList(),
                    CaseUpdateIds = c.CaseUpdates.Select(u => u.Id).ToList(),
                    DocumentIds = c.Documents.Select(d => d.Id).ToList(),
                    AppointmentIds = c.Appointments.Select(a => a.Id).ToList(),
                })
                .ToListAsync();
        }

        public async Task<CaseDto?> GetByIdAsync(int id)
        {
            var c = await _context
                .Cases.Include(x => x.Invoices)
                .Include(x => x.Expenses)
                .Include(x => x.CaseUpdates)
                .Include(x => x.Documents)
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (c == null)
                return null;
            return new CaseDto
            {
                Id = c.Id,
                CaseNumber = c.CaseNumber,
                Title = c.Title,
                Description = c.Description,
                Status = c.Status.ToString(),
                OpenDate = c.OpenDate,
                ClientId = c.ClientId,
                InvoiceIds = c.Invoices.Select(i => i.Id).ToList(),
                ExpenseIds = c.Expenses.Select(e => e.Id).ToList(),
                CaseUpdateIds = c.CaseUpdates.Select(u => u.Id).ToList(),
                DocumentIds = c.Documents.Select(d => d.Id).ToList(),
                AppointmentIds = c.Appointments.Select(a => a.Id).ToList(),
            };
        }

        public async Task<CaseDto> CreateAsync(CreateCaseDto dto)
        {
            var status = Enum.TryParse<CaseStatus>(dto.Status, out var parsedStatus)
                ? parsedStatus
                : CaseStatus.Open;
            var entity = new Case
            {
                CaseNumber = dto.CaseNumber,
                Title = dto.Title,
                Description = dto.Description,
                Status = status,
                OpenDate = dto.OpenDate,
                ClientId = dto.ClientId,
            };
            _context.Cases.Add(entity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(entity.Id) ?? throw new Exception("Case creation failed");
        }

        public async Task<bool> UpdateAsync(int id, UpdateCaseDto dto)
        {
            var entity = await _context.Cases.FindAsync(id);
            if (entity == null)
                return false;
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Status = Enum.TryParse<CaseStatus>(dto.Status, out var parsedStatus)
                ? parsedStatus
                : entity.Status;
            entity.OpenDate = dto.OpenDate;
            entity.ClientId = dto.ClientId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Cases.FindAsync(id);
            if (entity == null)
                return false;
            _context.Cases.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultDto<CaseDto>> GetAllCasesAsync(CaseQueryParametersDto query)
        {
            var cases = _context.Cases.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                cases = cases.Where(c =>
                    c.CaseNumber.Contains(query.Search) || c.Title.Contains(query.Search)
                );
            }
            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                if (Enum.TryParse<CaseStatus>(query.Status, out var status))
                    cases = cases.Where(c => c.Status == status);
            }
            if (query.ClientId.HasValue)
            {
                cases = cases.Where(c => c.ClientId == query.ClientId);
            }
            var totalCount = await cases.CountAsync();
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = query.PageSize < 1 ? 20 : query.PageSize;
            var items = await cases
                .OrderByDescending(c => c.OpenDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CaseDto
                {
                    Id = c.Id,
                    CaseNumber = c.CaseNumber,
                    Title = c.Title,
                    Description = c.Description,
                    Status = c.Status.ToString(),
                    OpenDate = c.OpenDate,
                    ClientId = c.ClientId,
                    InvoiceIds = c.Invoices.Select(i => i.Id).ToList(),
                    ExpenseIds = c.Expenses.Select(e => e.Id).ToList(),
                    CaseUpdateIds = c.CaseUpdates.Select(u => u.Id).ToList(),
                    DocumentIds = c.Documents.Select(d => d.Id).ToList(),
                    AppointmentIds = c.Appointments.Select(a => a.Id).ToList(),
                })
                .ToListAsync();
            return new PagedResultDto<CaseDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
            };
        }
    }
}
