using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawFirm.Application.Modules.Organizations;
using LawFirm.Application.Modules.Organizations.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Organizations;
using LawFirm.Domain.Modules.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly LawFirmDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrganizationService(
            LawFirmDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto)
        {
            var org = new Organization { Name = dto.Name };
            _context.Organizations.Add(org);
            await _context.SaveChangesAsync();
            return new OrganizationDto
            {
                Id = org.Id,
                Name = org.Name,
                CreatedAt = org.CreatedAt,
            };
        }

        public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
        {
            return await _context
                .Organizations.Select(o => new OrganizationDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    CreatedAt = o.CreatedAt,
                })
                .ToListAsync();
        }

        public async Task<OrganizationDto?> GetByIdAsync(int id)
        {
            var o = await _context.Organizations.FindAsync(id);
            if (o == null)
                return null;
            return new OrganizationDto
            {
                Id = o.Id,
                Name = o.Name,
                CreatedAt = o.CreatedAt,
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrganizationDto dto)
        {
            var o = await _context.Organizations.FindAsync(id);
            if (o == null)
                return false;
            o.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var o = await _context.Organizations.FindAsync(id);
            if (o == null)
                return false;
            _context.Organizations.Remove(o);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignUserAsync(AssignUserToOrganizationDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return false;
            user.OrganizationId = dto.OrganizationId;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> RemoveUserAsync(AssignUserToOrganizationDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return false;
            user.OrganizationId = 0;
            await _userManager.UpdateAsync(user);
            return true;
        }
    }
}
