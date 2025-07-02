using System;

namespace LawFirm.Application.Modules.Invoices.Dto
{
    public class CreateInvoiceDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CaseId { get; set; }
    }
}
