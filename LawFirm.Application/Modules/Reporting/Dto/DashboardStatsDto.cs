namespace LawFirm.Application.Modules.Reporting.Dto
{
    public class DashboardStatsDto
    {
        public int TotalClients { get; set; }
        public int TotalCases { get; set; }
        public int TotalInvoices { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OverdueInvoices { get; set; }
    }
}
