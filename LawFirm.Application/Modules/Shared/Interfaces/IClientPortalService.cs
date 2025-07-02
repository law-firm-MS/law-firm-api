using LawFirm.Application.Modules.Cases.Dto;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IClientPortalService
    {
        Task<ClientProfileDto?> GetMyProfileAsync(string userId);
        Task<IEnumerable<CaseDto>> GetMyCasesAsync(int clientId);
        Task<CaseDto?> GetMyCaseByIdAsync(int clientId, int caseId);
    }
}
