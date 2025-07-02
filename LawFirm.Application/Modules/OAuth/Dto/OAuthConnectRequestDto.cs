namespace LawFirm.Application.Modules.OAuth.Dto
{
    public class OAuthConnectRequestDto
    {
        public string Provider { get; set; } = string.Empty; // "Google" or "Microsoft"
        public List<string> Scopes { get; set; } = new();
    }
}
