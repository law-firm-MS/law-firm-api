using System;
using System.Collections.Generic;

namespace LawFirm.Application.Modules.Cases.Dto
{
    public class CaseDto
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime OpenDate { get; set; }
        public int ClientId { get; set; }
        public List<int> InvoiceIds { get; set; } = new();
        public List<int> ExpenseIds { get; set; } = new();
        public List<int> CaseUpdateIds { get; set; } = new();
        public List<int> DocumentIds { get; set; } = new();
        public List<int> AppointmentIds { get; set; } = new();
    }
}
