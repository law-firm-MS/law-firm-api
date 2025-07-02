using LawFirm.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("search")]
    [Authorize]
    public class GlobalSearchController : ControllerBase
    {
        private readonly IGlobalSearchService _service;

        public GlobalSearchController(IGlobalSearchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query is required.");
            var results = await _service.SearchAsync(query);
            return Ok(results);
        }
    }
}
