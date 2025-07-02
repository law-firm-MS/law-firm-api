using System;
using LawFirm.Domain.Modules.Cases;

namespace LawFirm.Domain.Modules.Invoices
{
    public enum InvoiceStatus
    {
        Open,
        Draft,
        Sent,
        Paid,
    }

    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; }

        public int CaseId { get; set; }
        public Case? Case { get; set; }

        public int OrganizationId { get; set; }
    }
}
