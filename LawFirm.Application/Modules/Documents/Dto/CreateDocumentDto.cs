using System;

namespace LawFirm.Application.Modules.Documents.Dto
{
    public class CreateDocumentDto
    {
        public string FileName { get; set; } = string.Empty;
        public string CloudStorageUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
    }
}
