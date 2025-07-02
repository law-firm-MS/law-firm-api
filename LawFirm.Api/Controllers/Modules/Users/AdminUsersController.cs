using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Application.Modules.Users;
using LawFirm.Application.Modules.Users.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        public AdminUsersController(IUserManagementService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var user = await _userService.CreateUserAsync(dto);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] UpdateUserDto dto)
        {
            var updated = await _userService.UpdateUserAsync(userId, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var deleted = await _userService.DeleteUserAsync(userId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("organization-users")]
        [Authorize(Roles = "Admin,OrgAdmin")]
        public async Task<IActionResult> GetOrganizationUsers()
        {
            var orgId = (int)(HttpContext.Items["OrganizationId"] ?? 0);
            var users = await _userService.GetUsersByOrganizationAsync(orgId);
            return Ok(users);
        }
    }
}
