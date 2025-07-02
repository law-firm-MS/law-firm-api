using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IClientServiceRequestService
    {
        Task<ClientServiceDto> RequestServiceAsync(string clientEmail, ServiceRequestDto dto);
        Task<IEnumerable<ClientServiceDto>> GetClientServicesAsync(string clientEmail);
        Task<ClientServiceDto?> GetClientServiceByIdAsync(string clientEmail, int serviceId);
    }
}
