using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
    public class CholesterolGuideline : BaseModel
    {
        public Guid GuidelineId { get; set; }
        public Guideline Guideline { get; set; }
        public List<ValueGuideline> ValueGuidelines { get; set; } = [];
    }
    public class CholesterolGuidelineConfiguration : EntityTypeConfiguration<CholesterolGuideline>
    {
        public void Configure(EntityTypeBuilder<CholesterolGuideline> builder)
        {
            builder.HasMany(a => a.ValueGuidelines).WithOne().HasForeignKey(a => a.CholesterolGuidelineId).IsRequired(true);
        }
    }
}
