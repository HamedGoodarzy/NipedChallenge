using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
    public class ValueGuideline : BaseModel
    {
        public Guid GuidelineId { get; set; }
        public Guideline Guideline { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }
}
