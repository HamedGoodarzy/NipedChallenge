namespace WebApplication1.Models
{
    public class GuidelineSet
    {
        public CholesterolGuideline Cholesterol { get; set; } = new CholesterolGuideline();
        public ValueGuideline BloodSugar { get; set; } = new ValueGuideline();
        public BloodPressureGuideline BloodPressure { get; set; } = new BloodPressureGuideline();
        public ValueGuideline ExerciseWeeklyMinutes { get; set; } = new ValueGuideline();
        public TextGuideline SleepQuality { get; set; } = new TextGuideline();
        public TextGuideline StressLevels { get; set; } = new TextGuideline();
        public TextGuideline DietQuality { get; set; } = new TextGuideline();
    }

    public class CholesterolGuideline
    {
        public ValueGuideline Total { get; set; } = new ValueGuideline();
        public ValueGuideline Hdl { get; set; } = new ValueGuideline();
        public ValueGuideline Ldl { get; set; } = new ValueGuideline();
    }

    public class ValueGuideline
    {
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }

    public class TextGuideline
    {
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }

    public class BloodPressureGuideline
    {
        public BloodPressureThreshold Optimal { get; set; } = new BloodPressureThreshold(); 
        public BloodPressureThreshold NeedsAttention { get; set; } = new BloodPressureThreshold();
        public BloodPressureThreshold SeriousIssue { get; set; } = new BloodPressureThreshold();
    }

    public class BloodPressureThreshold
    {
        public string Systolic { get; set; } = string.Empty;
        public string Diastolic { get; set; } = string.Empty;
    }
}
