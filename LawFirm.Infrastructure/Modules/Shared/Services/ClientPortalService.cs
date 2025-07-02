using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Cases;
using LawFirm.Application.Modules.Cases.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Cases;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class ClientPortalService : IClientPortalService
    {
        private readonly LawFirmDbContext _context;

        public ClientPortalService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<ClientProfileDto?> GetMyProfileAsync(string userId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == userId);
            if (client == null)
                return null;
            return new ClientProfileDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                Address = client.Address,
            };
        }

        public async Task<IEnumerable<CaseDto>> GetMyCasesAsync(int clientId)
        {
            return await _context
                .Cases.Where(c => c.ClientId == clientId)
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
        }

        public async Task<CaseDto?> GetMyCaseByIdAsync(int clientId, int caseId)
        {
            var c = await _context
                .Cases.Include(x => x.Invoices)
                .Include(x => x.Expenses)
                .Include(x => x.CaseUpdates)
                .Include(x => x.Documents)
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == caseId && x.ClientId == clientId);
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
    }
}
