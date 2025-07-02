using Microsoft.AspNetCore.Identity;

namespace LawFirm.Domain.Modules.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
    }
}
