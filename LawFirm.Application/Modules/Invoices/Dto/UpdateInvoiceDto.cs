using System;

namespace LawFirm.Application.Modules.Invoices.Dto
{
    public class UpdateInvoiceDto
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
