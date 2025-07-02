using LawFirm.Application.Modules.Clients;
using LawFirm.Application.Modules.Clients.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain.Modules.Users;
using LawFirm.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Api.Controllers.Modules.Clients
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Lawyer")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LawFirmDbContext _context;

        public ClientsController(
            IClientService clientService,
            UserManager<ApplicationUser> userManager,
            LawFirmDbContext context
        )
        {
            _clientService = clientService;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ClientQueryParametersDto query)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var result = await _clientService.GetAllClientsAsync(query, user.OrganizationId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var client = await _clientService.GetByIdAsync(id, user.OrganizationId);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var client = await _clientService.CreateAsync(dto, user.OrganizationId);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClientDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var updated = await _clientService.UpdateAsync(id, dto, user.OrganizationId);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var deleted = await _clientService.DeleteAsync(id, user.OrganizationId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterClientDto dto)
        {
            // Lookup org by key
            var org = await _context.Organizations.FirstOrDefaultAsync(o =>
                o.OrganizationKey == dto.OrganizationKey
            );
            if (org == null)
                return BadRequest("Invalid organization key.");

            // Create user and assign org
            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                OrganizationId = org.Id,
                // ... other fields
            };
            // ... create user, assign role, etc.
            // (Assume you have a UserManager injected)
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await _userManager.AddToRoleAsync(user, "Client");
            // ... create Client entity, etc.
            return Ok();
        }
    }
}
