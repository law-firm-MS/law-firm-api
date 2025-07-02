using System;
using System.Collections.Generic;

namespace LawFirm.Application.Modules.Shared.Dto
{
    public class ClientServiceDto
    {
        public int Id { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime OpenDate { get; set; }
        public List<string>? Attachments { get; set; }
    }
}
