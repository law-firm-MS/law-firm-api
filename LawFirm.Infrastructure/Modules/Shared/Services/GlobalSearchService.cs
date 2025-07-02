using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class GlobalSearchService : IGlobalSearchService
    {
        private readonly LawFirmDbContext _context;

        public GlobalSearchService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GlobalSearchResultDto>> SearchAsync(string query)
        {
            var results = new List<GlobalSearchResultDto>();
            // Clients
            results.AddRange(
                await _context
                    .Clients.Where(c => c.Name.Contains(query) || c.Email.Contains(query))
                    .Select(c => new GlobalSearchResultDto
                    {
                        EntityType = "Client",
                        Id = c.Id.ToString(),
                        Title = c.Name,
                        Snippet = c.Email,
                    })
                    .ToListAsync()
            );
            // Cases
            results.AddRange(
                await _context
                    .Cases.Where(c =>
                        c.CaseNumber.Contains(query)
                        || c.Title.Contains(query)
                        || c.Description.Contains(query)
                    )
                    .Select(c => new GlobalSearchResultDto
                    {
                        EntityType = "Case",
                        Id = c.Id.ToString(),
                        Title = c.Title,
                        Snippet = c.Description,
                    })
                    .ToListAsync()
            );
            // Documents
            results.AddRange(
                await _context
                    .Documents.Where(d => d.FileName.Contains(query))
                    .Select(d => new GlobalSearchResultDto
                    {
                        EntityType = "Document",
                        Id = d.Id.ToString(),
                        Title = d.FileName,
                        Snippet = d.FileType,
                    })
                    .ToListAsync()
            );
            return results;
        }
    }
}
