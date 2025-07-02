namespace LawFirm.Application.Modules.Users.Dto
{
    public class UpdateUserDto
    {
        public string Role { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
