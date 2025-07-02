using LawFirm.Application.Modules.Clients.Dto;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Modules.Clients
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<ClientDto?> GetByIdAsync(int id, int organizationId);
        Task<ClientDto> CreateAsync(CreateClientDto dto, int organizationId);
        Task<bool> UpdateAsync(int id, UpdateClientDto dto, int organizationId);
        Task<bool> DeleteAsync(int id, int organizationId);
        Task<PagedResultDto<ClientDto>> GetAllClientsAsync(
            ClientQueryParametersDto query,
            int organizationId
        );
    }
}
