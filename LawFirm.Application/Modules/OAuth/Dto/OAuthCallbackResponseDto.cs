namespace LawFirm.Application.Modules.OAuth.Dto
{
    public class OAuthCallbackResponseDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Provider { get; set; }
        public DateTime? Expiry { get; set; }
        public string? Error { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
    }
}
