using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
    public class Guideline : BaseModel
    {
        //public string? TestField { get; set; } 
        public List<ValueGuideline>? ValueGuidelines { get; set; }

        //public ValueGuideline BloodSugar { get; set; } = new ValueGuideline();
        //public BloodPressureGuideline BloodPressure { get; set; } = new BloodPressureGuideline();
        //public ValueGuideline ExerciseWeeklyMinutes { get; set; } = new ValueGuideline();
        //public TextGuideline SleepQuality { get; set; } = new TextGuideline();
        //public TextGuideline StressLevels { get; set; } = new TextGuideline();
        //public TextGuideline DietQuality { get; set; } = new TextGuideline();
    }

    public class GuidelineConfiguration : EntityTypeConfiguration<Guideline>
    {
        public void Configure(EntityTypeBuilder<Guideline> builder)
        {
            builder.HasMany(a => a.ValueGuidelines).WithOne().HasForeignKey(a => a.GuidelineId).IsRequired(true);
        }
    }
}
