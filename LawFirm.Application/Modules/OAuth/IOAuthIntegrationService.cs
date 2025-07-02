using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LawFirm.Application.Modules.OAuth.Dto;
using LawFirm.Domain.Modules.OAuth;

namespace LawFirm.Application.Interfaces
{
    public interface IOAuthIntegrationService
    {
        Task StoreTokenAsync(
            string userId,
            string provider,
            string accessToken,
            string refreshToken,
            DateTime expiry
        );
        Task<OAuthToken?> GetTokenAsync(string userId, string provider);
        Task<string?> RefreshTokenAsync(string userId, string provider);
        string GetOAuthRedirectUrl(string provider, string userId, List<string> scopes);
        Task<OAuthCallbackResponseDto> HandleOAuthCallbackAsync(
            string provider,
            string code,
            string userId
        );
    }
}
