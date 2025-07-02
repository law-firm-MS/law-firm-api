namespace LawFirm.Application.Modules.Shared.Dto
{
    public class AssignUserToOrganizationDto
    {
        public string UserId { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
    }
}
