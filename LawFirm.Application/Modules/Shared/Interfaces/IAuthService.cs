using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterClientAsync(RegisterClientDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
