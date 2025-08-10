using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ClientReportVM
    {
        public Client Client { get; set; } = new Client();
        public List<ReportEntry> ReportEntries { get; set; } = new List<ReportEntry>();
    }
}
