using System.Collections.Generic;
using System.Threading.Tasks;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IGlobalSearchService
    {
        Task<IEnumerable<GlobalSearchResultDto>> SearchAsync(string query);
    }
}
