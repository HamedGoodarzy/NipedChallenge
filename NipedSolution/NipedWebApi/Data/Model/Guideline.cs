using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
    public class Guideline : BaseModel
    {
        //public string? TestField { get; set; } 
        public virtual CholesterolGuideline? Cholesterol { get; set; }

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
            builder.HasOne(e => e.Cholesterol)
            .WithOne(e => e.Guideline)
            .HasForeignKey<CholesterolGuideline>(e => e.GuidelineId)
            .IsRequired();
        }
    }
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


    public class ValueGuideline : BaseModel
    {
        public Guid CholesterolGuidelineId { get; set; }
        public CholesterolGuideline CholesterolGuideline { get; set; }

        public string Label { get; set; } = string.Empty;
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }

    public class TextGuideline : BaseModel
    {
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }

    //public class BloodPressureGuideline : BaseModel
    //{
        //public BloodPressureThreshold Optimal { get; set; } = new BloodPressureThreshold();
        //public BloodPressureThreshold NeedsAttention { get; set; } = new BloodPressureThreshold();
        //public BloodPressureThreshold SeriousIssue { get; set; } = new BloodPressureThreshold();
    //}

    //public class BloodPressureThreshold : BaseModel
    //{
        //public string Systolic { get; set; } = string.Empty;
        //public string Diastolic { get; set; } = string.Empty;
    //}
}
