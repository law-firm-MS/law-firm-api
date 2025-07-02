using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using LawFirm.Domain;
using LawFirm.Domain.Modules.Shared;
using Microsoft.EntityFrameworkCore;

namespace LawFirm.Infrastructure.Services
{
    public class CaseUpdateService : ICaseUpdateService
    {
        private readonly LawFirmDbContext _context;

        public CaseUpdateService(LawFirmDbContext context)
        {
            _context = context;
        }

        public async Task<CaseUpdateDto> CreateAsync(int caseId, CreateCaseUpdateDto dto)
        {
            var update = new CaseUpdate
            {
                Date = dto.Date,
                Description = dto.Description,
                CaseId = caseId,
            };
            _context.CaseUpdates.Add(update);
            await _context.SaveChangesAsync();
            return new CaseUpdateDto
            {
                Id = update.Id,
                Date = update.Date,
                Description = update.Description,
                CaseId = update.CaseId,
            };
        }
    }
}
