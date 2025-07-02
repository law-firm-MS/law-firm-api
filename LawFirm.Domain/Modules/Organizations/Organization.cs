using System;

namespace LawFirm.Domain.Modules.Organizations
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string OrganizationKey { get; set; } = Guid.NewGuid().ToString("N");
    }
}
