using System;

namespace LawFirm.Application.Modules.Documents.Dto
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string CloudStorageUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public int CaseId { get; set; }
    }
}
