using System.Security.Claims;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize(Roles = "Client")]
    public class ClientServicesController : ControllerBase
    {
        private readonly IClientServiceRequestService _serviceRequestService;

        public ClientServicesController(IClientServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpPost("services/request")]
        public async Task<IActionResult> RequestService([FromBody] ServiceRequestDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (email == null)
                return Unauthorized();
            var result = await _serviceRequestService.RequestServiceAsync(email, dto);
            return Ok(result);
        }

        [HttpGet("clients/{clientId}/services")]
        public async Task<IActionResult> GetClientServices(int clientId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (email == null)
                return Unauthorized();
            // Only allow access to own services
            // In a real system, clientId should be validated against the logged-in user's id
            var result = await _serviceRequestService.GetClientServicesAsync(email);
            return Ok(result);
        }

        [HttpGet("services/{serviceId}")]
        public async Task<IActionResult> GetServiceById(int serviceId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (email == null)
                return Unauthorized();
            var result = await _serviceRequestService.GetClientServiceByIdAsync(email, serviceId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
