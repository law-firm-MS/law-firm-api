using System;
using System.Collections.Generic;
using LawFirm.Domain.Modules.Appointments;
using LawFirm.Domain.Modules.Clients;
using LawFirm.Domain.Modules.Documents;
using LawFirm.Domain.Modules.Expenses;
using LawFirm.Domain.Modules.Invoices;
using LawFirm.Domain.Modules.Shared;

namespace LawFirm.Domain.Modules.Cases
{
    public enum CaseStatus
    {
        Open,
        Closed,
        Pending,
    }

    public class Case
    {
        public int Id { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CaseStatus Status { get; set; }
        public DateTime OpenDate { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public int OrganizationId { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<CaseUpdate> CaseUpdates { get; set; } = new List<CaseUpdate>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
