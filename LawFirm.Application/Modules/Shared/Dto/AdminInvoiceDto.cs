using System;

namespace LawFirm.Application.Modules.Shared.Dto
{
    public class AdminInvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int CaseId { get; set; }
        public int ClientId { get; set; }
    }
}
