using System;
using LawFirm.Domain.Modules.Cases;

namespace LawFirm.Domain.Modules.Appointments
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? MeetingLink { get; set; }

        public int CaseId { get; set; }
        public Cases.Case? Case { get; set; }

        public int OrganizationId { get; set; }
    }
}
