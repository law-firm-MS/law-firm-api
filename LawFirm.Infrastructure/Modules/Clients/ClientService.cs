using LawFirm.Application.Modules.Clients;
using LawFirm.Application.Modules.Clients.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain.Modules.Clients;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly LawFirmDbContext _context;

        public ClientService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            return await _context
                .Clients.Select(c => new ClientDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                })
                .ToListAsync();
        }

        public async Task<ClientDto?> GetByIdAsync(int id, int organizationId)
        {
            var c = await _context.Clients.FirstOrDefaultAsync(x =>
                x.Id == id && x.OrganizationId == organizationId
            );
            if (c == null)
                return null;
            return new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
            };
        }

        public async Task<ClientDto> CreateAsync(CreateClientDto dto, int organizationId)
        {
            var client = new Client
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                OrganizationId = organizationId,
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                Address = client.Address,
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateClientDto dto, int organizationId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x =>
                x.Id == id && x.OrganizationId == organizationId
            );
            if (client == null)
                return false;
            client.Name = dto.Name;
            client.Email = dto.Email;
            client.PhoneNumber = dto.PhoneNumber;
            client.Address = dto.Address;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int organizationId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x =>
                x.Id == id && x.OrganizationId == organizationId
            );
            if (client == null)
                return false;
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultDto<ClientDto>> GetAllClientsAsync(
            ClientQueryParametersDto query,
            int organizationId
        )
        {
            var clients = _context.Clients.Where(x => x.OrganizationId == organizationId);
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                clients = clients.Where(c => c.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                clients = clients.Where(c => c.Email.Contains(query.Email));
            }
            var totalCount = await clients.CountAsync();
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = query.PageSize < 1 ? 20 : query.PageSize;
            var items = await clients
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                })
                .ToListAsync();
            return new PagedResultDto<ClientDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
            };
        }
    }
}
