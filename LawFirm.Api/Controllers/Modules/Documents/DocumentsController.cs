using LawFirm.Application.Interfaces;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Documents;
using LawFirm.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("documents")]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly LawFirmDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public DocumentsController(LawFirmDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] int caseId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            var filePath = await _fileStorageService.SaveFileAsync(file, $"case_{caseId}");
            var doc = new Document
            {
                FileName = file.FileName,
                FileType = file.ContentType,
                UploadDate = DateTime.UtcNow,
                CaseId = caseId,
                CloudStorageUrl = filePath, // For now, store local path
            };
            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();
            return Ok(new { doc.Id, doc.FileName });
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null)
                return NotFound();
            var bytes = await _fileStorageService.GetFileAsync(doc.CloudStorageUrl);
            return File(bytes, doc.FileType, doc.FileName);
        }
    }
}
