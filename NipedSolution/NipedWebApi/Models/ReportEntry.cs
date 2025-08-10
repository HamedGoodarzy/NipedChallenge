using NipedWebApi.Data.Model;

namespace WebApplication1.Models
{
    public class ReportEntry : BaseModel
    {
        public string MetricPath { get; set; }
        public object Value { get; set; }
        public string Category { get; set; }
        public string Explanation { get; set; }
    }
}
