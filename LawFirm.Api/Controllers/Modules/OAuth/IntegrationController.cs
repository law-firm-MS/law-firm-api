using System.Threading.Tasks;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.OAuth;
using LawFirm.Application.Modules.OAuth.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("api/integrations")]
    [Authorize]
    public class IntegrationController : ControllerBase
    {
        private readonly IOAuthIntegrationService _oauthService;

        public IntegrationController(IOAuthIntegrationService oauthService)
        {
            _oauthService = oauthService;
        }

        [HttpPost("google/connect")]
        public IActionResult ConnectGoogle([FromBody] OAuthConnectRequestDto dto)
        {
            var userId = User.Identity?.Name ?? "";
            var url = _oauthService.GetOAuthRedirectUrl("Google", userId, dto.Scopes);
            return Ok(new { redirectUrl = url });
        }

        [HttpPost("microsoft/connect")]
        public IActionResult ConnectMicrosoft([FromBody] OAuthConnectRequestDto dto)
        {
            var userId = User.Identity?.Name ?? "";
            var url = _oauthService.GetOAuthRedirectUrl("Microsoft", userId, dto.Scopes);
            return Ok(new { redirectUrl = url });
        }

        [HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback(
            [FromQuery] string code,
            [FromQuery] string state
        )
        {
            var result = await _oauthService.HandleOAuthCallbackAsync("Google", code, state);
            return Ok(result);
        }

        [HttpGet("microsoft/callback")]
        public async Task<IActionResult> MicrosoftCallback(
            [FromQuery] string code,
            [FromQuery] string state
        )
        {
            var result = await _oauthService.HandleOAuthCallbackAsync("Microsoft", code, state);
            return Ok(result);
        }
    }
}
