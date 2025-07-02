using System;

namespace LawFirm.Application.Modules.Invoices.Dto
{
    public class InvoiceQueryParametersDto
    {
        public string? Search { get; set; }
        public string? Status { get; set; }
        public int? ClientId { get; set; }
        public int? CaseId { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
