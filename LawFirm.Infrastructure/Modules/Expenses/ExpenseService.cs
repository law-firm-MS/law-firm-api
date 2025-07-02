using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Expenses;
using LawFirm.Application.Modules.Expenses.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Expenses;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly LawFirmDbContext _context;

        public ExpenseService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExpenseDto>> GetByCaseIdAsync(int caseId)
        {
            return await _context
                .Expenses.Where(e => e.CaseId == caseId)
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    Date = e.Date,
                    CaseId = e.CaseId,
                })
                .ToListAsync();
        }

        public async Task<ExpenseDto> CreateAsync(int caseId, CreateExpenseDto dto)
        {
            var expense = new Expense
            {
                Description = dto.Description,
                Amount = dto.Amount,
                Date = dto.Date,
                CaseId = caseId,
            };
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return new ExpenseDto
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CaseId = expense.CaseId,
            };
        }
    }
}
