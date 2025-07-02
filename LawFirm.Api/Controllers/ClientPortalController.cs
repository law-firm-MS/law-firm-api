using System.Security.Claims;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Client")]
    public class ClientPortalController : ControllerBase
    {
        private readonly IClientPortalService _portalService;

        public ClientPortalController(IClientPortalService portalService)
        {
            _portalService = portalService;
        }

        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (userEmail == null)
                return Unauthorized();
            var profile = await _portalService.GetMyProfileAsync(userEmail);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        [HttpGet("my-cases")]
        public async Task<IActionResult> GetMyCases()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (userEmail == null)
                return Unauthorized();
            var profile = await _portalService.GetMyProfileAsync(userEmail);
            if (profile == null)
                return NotFound();
            var cases = await _portalService.GetMyCasesAsync(profile.Id);
            return Ok(cases);
        }

        [HttpGet("my-cases/{caseId}")]
        public async Task<IActionResult> GetMyCaseById(int caseId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (userEmail == null)
                return Unauthorized();
            var profile = await _portalService.GetMyProfileAsync(userEmail);
            if (profile == null)
                return NotFound();
            var c = await _portalService.GetMyCaseByIdAsync(profile.Id, caseId);
            if (c == null)
                return NotFound();
            return Ok(c);
        }
    }
}
