using LawFirm.Application.Modules.Expenses.Dto;
using LawFirm.Application.Modules.Shared.Dto;

namespace LawFirm.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetByCaseIdAsync(int caseId);
        Task<ExpenseDto> CreateAsync(int caseId, CreateExpenseDto dto);
    }
}
