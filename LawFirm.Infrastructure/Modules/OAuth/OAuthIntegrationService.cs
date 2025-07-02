using System;
using System.Threading.Tasks;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.OAuth.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.OAuth;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class OAuthIntegrationService : IOAuthIntegrationService
    {
        private readonly LawFirmDbContext _context;

        public OAuthIntegrationService(LawFirmDbContext context)
        {
            _context = context;
        }

        public string GetOAuthRedirectUrl(string provider, string userId, List<string> scopes)
        {
            // TODO: Build and return the provider's OAuth consent URL with state=userId
            return $"https://example.com/oauth/{provider.ToLower()}/consent?user={userId}";
        }

        public async Task<OAuthCallbackResponseDto> HandleOAuthCallbackAsync(
            string provider,
            string code,
            string userId
        )
        {
            // TODO: Exchange code for tokens, store refresh_token securely for userId
            // This is where you would call Google/Microsoft token endpoint
            await Task.CompletedTask;
            return new OAuthCallbackResponseDto
            {
                Success = true,
                Message = $"{provider} OAuth callback handled (stub).",
                Provider = provider,
                UserId = userId,
            };
        }

        public async Task StoreTokenAsync(
            string userId,
            string provider,
            string accessToken,
            string refreshToken,
            DateTime expiry
        )
        {
            var existing = await _context.OAuthTokens.FirstOrDefaultAsync(t =>
                t.UserId == userId && t.Provider == provider
            );
            if (existing != null)
            {
                existing.AccessToken = accessToken;
                existing.RefreshToken = refreshToken;
                existing.Expiry = expiry;
            }
            else
            {
                _context.OAuthTokens.Add(
                    new OAuthToken
                    {
                        UserId = userId,
                        Provider = provider,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        Expiry = expiry,
                    }
                );
            }
            await _context.SaveChangesAsync();
        }

        public async Task<OAuthToken?> GetTokenAsync(string userId, string provider)
        {
            return await _context.OAuthTokens.FirstOrDefaultAsync(t =>
                t.UserId == userId && t.Provider == provider
            );
        }

        public async Task<string?> RefreshTokenAsync(string userId, string provider)
        {
            // This is a stub. In a real implementation, call the provider's token endpoint.
            var token = await GetTokenAsync(userId, provider);
            if (token == null || string.IsNullOrEmpty(token.RefreshToken))
                return null;
            // Simulate refresh: just extend expiry and return same access token
            token.Expiry = DateTime.UtcNow.AddHours(1);
            await _context.SaveChangesAsync();
            return token.AccessToken;
        }
    }
}
