using System.Collections.Generic;
using System.Threading.Tasks;
using LawFirm.Application.Modules.Organizations.Dto;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Modules.Organizations
{
    public interface IOrganizationService
    {
        Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto);
        Task<IEnumerable<OrganizationDto>> GetAllAsync();
        Task<OrganizationDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateOrganizationDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AssignUserAsync(AssignUserToOrganizationDto dto);
        Task<bool> RemoveUserAsync(AssignUserToOrganizationDto dto);
    }
}
