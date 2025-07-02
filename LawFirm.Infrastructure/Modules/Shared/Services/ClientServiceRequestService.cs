using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Cases;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class ClientServiceRequestService : IClientServiceRequestService
    {
        private readonly LawFirmDbContext _context;

        public ClientServiceRequestService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<ClientServiceDto> RequestServiceAsync(
            string clientEmail,
            ServiceRequestDto dto
        )
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == clientEmail);
            if (client == null)
                throw new Exception("Client not found");
            var entity = new Case
            {
                CaseNumber = Guid.NewGuid().ToString(),
                Title = dto.ServiceType,
                Description = dto.Description,
                Status = CaseStatus.Open,
                OpenDate = DateTime.UtcNow,
                ClientId = client.Id,
                // Attachments: stub, not implemented in model
            };
            _context.Cases.Add(entity);
            await _context.SaveChangesAsync();
            return new ClientServiceDto
            {
                Id = entity.Id,
                ServiceType = entity.Title,
                Description = entity.Description,
                Status = entity.Status.ToString(),
                OpenDate = entity.OpenDate,
                Attachments = dto.Attachments,
            };
        }

        public async Task<IEnumerable<ClientServiceDto>> GetClientServicesAsync(string clientEmail)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == clientEmail);
            if (client == null)
                return Enumerable.Empty<ClientServiceDto>();
            return await _context
                .Cases.Where(c => c.ClientId == client.Id)
                .Select(c => new ClientServiceDto
                {
                    Id = c.Id,
                    ServiceType = c.Title,
                    Description = c.Description,
                    Status = c.Status.ToString(),
                    OpenDate = c.OpenDate,
                    Attachments = new List<string>(), // stub
                })
                .ToListAsync();
        }

        public async Task<ClientServiceDto?> GetClientServiceByIdAsync(
            string clientEmail,
            int serviceId
        )
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == clientEmail);
            if (client == null)
                return null;
            var c = await _context.Cases.FirstOrDefaultAsync(x =>
                x.Id == serviceId && x.ClientId == client.Id
            );
            if (c == null)
                return null;
            return new ClientServiceDto
            {
                Id = c.Id,
                ServiceType = c.Title,
                Description = c.Description,
                Status = c.Status.ToString(),
                OpenDate = c.OpenDate,
                Attachments = new List<string>(), // stub
            };
        }
    }
}
