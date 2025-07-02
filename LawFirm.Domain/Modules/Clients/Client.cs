using System.Collections.Generic;
using LawFirm.Domain.Modules.Cases;

namespace LawFirm.Domain.Modules.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public ICollection<Case> Cases { get; set; } = new List<Case>();
    }
}
