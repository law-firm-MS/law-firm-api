using System;

namespace LawFirm.Application.Modules.Expenses.Dto
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CaseId { get; set; }
    }
}
