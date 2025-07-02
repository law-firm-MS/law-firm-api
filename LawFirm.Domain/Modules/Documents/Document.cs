using System;
using LawFirm.Domain.Modules.Cases;

namespace LawFirm.Domain.Modules.Documents
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string CloudStorageUrl { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }

        public int CaseId { get; set; }
        public Cases.Case? Case { get; set; }

        public int OrganizationId { get; set; }
    }
}
