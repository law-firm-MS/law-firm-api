using System.Threading.Tasks;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Users;
using Microsoft.AspNetCore.Identity;

namespace LawFirm.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LawFirmDbContext _context;

        public UserProfileService(
            UserManager<ApplicationUser> userManager,
            LawFirmDbContext context
        )
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> UpdateProfileAsync(string userId, UpdateProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            user.UserName = dto.Email;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            // If you have Name/Address fields, set them here
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword
            );
            return result.Succeeded;
        }
    }
}
