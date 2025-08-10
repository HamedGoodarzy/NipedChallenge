namespace NipedModel
{
    public class GuidelineTO
    {
        public CholesterolGuidelineTO Cholesterol { get; set; } = new CholesterolGuidelineTO();
        public ValueGuidelineTO BloodSugar { get; set; } = new ValueGuidelineTO();
        public BloodPressureGuidelineTO BloodPressure { get; set; } = new BloodPressureGuidelineTO();
        public ValueGuidelineTO ExerciseWeeklyMinutes { get; set; } = new ValueGuidelineTO();
        public TextGuidelineTO SleepQuality { get; set; } = new TextGuidelineTO();
        public TextGuidelineTO StressLevels { get; set; } = new TextGuidelineTO();
        public TextGuidelineTO DietQuality { get; set; } = new TextGuidelineTO();
    }

    public class CholesterolGuidelineTO
    {
        public ValueGuidelineTO Total { get; set; } = new ValueGuidelineTO();
        public ValueGuidelineTO Hdl { get; set; } = new ValueGuidelineTO();
        public ValueGuidelineTO Ldl { get; set; } = new ValueGuidelineTO();
    }

    public class ValueGuidelineTO
    {
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }

    public class TextGuidelineTO
    {
        public string Optimal { get; set; } = string.Empty;
        public string NeedsAttention { get; set; } = string.Empty;
        public string SeriousIssue { get; set; } = string.Empty;
    }

    public class BloodPressureGuidelineTO
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
