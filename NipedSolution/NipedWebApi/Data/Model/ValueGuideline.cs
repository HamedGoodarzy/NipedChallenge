using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
    public class ValueGuideline : BaseModel
    {
        public Guid CholesterolGuidelineId { get; set; }
        public CholesterolGuideline CholesterolGuideline { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }
}
