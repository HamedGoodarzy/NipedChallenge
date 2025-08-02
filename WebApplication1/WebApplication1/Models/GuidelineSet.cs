namespace WebApplication1.Models
{
    public class GuidelineSet
    {
        public Dictionary<string, CholesterolGuideline> Cholesterol { get; set; }
        public ValueGuideline BloodSugar { get; set; }
        public BloodPressureGuideline BloodPressure { get; set; }
        public ValueGuideline ExerciseWeeklyMinutes { get; set; }
        public TextGuideline SleepQuality { get; set; }
        public TextGuideline StressLevels { get; set; }
        public TextGuideline DietQuality { get; set; }
    }

    public class CholesterolGuideline
    {
        public ValueGuideline Total { get; set; } 
        public ValueGuideline Hdl { get; set; }
        public ValueGuideline Ldl { get; set; }
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
        public BloodPressureThreshold Optimal { get; set; }
        public BloodPressureThreshold NeedsAttention { get; set; }
        public BloodPressureThreshold SeriousIssue { get; set; }
    }

    public class BloodPressureThreshold
    {
        public string Systolic { get; set; } = string.Empty;
        public string Diastolic { get; set; } = string.Empty;
    }
}
