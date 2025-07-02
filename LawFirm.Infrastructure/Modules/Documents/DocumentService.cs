using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Documents;
using LawFirm.Application.Modules.Documents.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Documents;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly LawFirmDbContext _context;

        public DocumentService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<DocumentDto> CreateAsync(int caseId, CreateDocumentDto dto)
        {
            var document = new Document
            {
                FileName = dto.FileName,
                CloudStorageUrl = dto.CloudStorageUrl,
                FileType = dto.FileType,
                UploadDate = dto.UploadDate,
                CaseId = caseId,
            };
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return new DocumentDto
            {
                Id = document.Id,
                FileName = document.FileName,
                CloudStorageUrl = document.CloudStorageUrl,
                FileType = document.FileType,
                UploadDate = document.UploadDate,
                CaseId = document.CaseId,
            };
        }
    }
}
