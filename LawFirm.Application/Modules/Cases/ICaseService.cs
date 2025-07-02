using LawFirm.Application.Modules.Cases.Dto;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Modules.Cases
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseDto>> GetAllAsync();
        Task<CaseDto?> GetByIdAsync(int id);
        Task<CaseDto> CreateAsync(CreateCaseDto dto);
        Task<bool> UpdateAsync(int id, UpdateCaseDto dto);
        Task<bool> DeleteAsync(int id);
        Task<PagedResultDto<CaseDto>> GetAllCasesAsync(CaseQueryParametersDto query);
    }
}
