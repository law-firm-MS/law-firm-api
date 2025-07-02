using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Organizations;
using LawFirm.Application.Modules.Organizations.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("organizations")]
    [Authorize(Roles = "Admin")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationsController(IOrganizationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrganizationDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpPost("assign-user")]
        public async Task<IActionResult> AssignUser([FromBody] AssignUserToOrganizationDto dto)
        {
            var success = await _service.AssignUserAsync(dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpPost("remove-user")]
        public async Task<IActionResult> RemoveUser([FromBody] AssignUserToOrganizationDto dto)
        {
            var success = await _service.RemoveUserAsync(dto);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
