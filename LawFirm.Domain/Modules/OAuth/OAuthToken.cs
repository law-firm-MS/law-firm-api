using System;

namespace LawFirm.Domain.Modules.OAuth
{
    public class OAuthToken
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty; // e.g., Google, Microsoft
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiry { get; set; }
    }
}
