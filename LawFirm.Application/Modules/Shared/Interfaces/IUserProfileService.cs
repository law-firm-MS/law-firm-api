using System.Threading.Tasks;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<bool> UpdateProfileAsync(string userId, UpdateProfileDto dto);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    }
}
