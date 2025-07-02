using LawFirm.Application.Modules.Documents.Dto;

namespace LawFirm.Application.Modules.Documents
{
    public interface IDocumentService
    {
        Task<DocumentDto> CreateAsync(int caseId, CreateDocumentDto dto);
    }
}
