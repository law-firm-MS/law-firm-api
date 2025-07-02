using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface ICaseUpdateService
    {
        Task<CaseUpdateDto> CreateAsync(int caseId, CreateCaseUpdateDto dto);
    }
}
