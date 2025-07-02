namespace LawFirm.Application.Modules.Shared.Dto
{
    public class RegisterClientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OrganizationKey { get; set; } = string.Empty;
    }
}
