using System;
using LawFirm.Domain.Modules.Cases;

namespace LawFirm.Domain.Modules.Expenses
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int CaseId { get; set; }
        public Cases.Case? Case { get; set; }

        public int OrganizationId { get; set; }
    }
}
