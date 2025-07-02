using LawFirm.Application.Modules.Users.Dto;

namespace LawFirm.Application.Modules.Users
{
    public interface IUserManagementService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(string userId, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(string userId);
        Task<IEnumerable<UserDto>> GetUsersByOrganizationAsync(int organizationId);
    }
}
