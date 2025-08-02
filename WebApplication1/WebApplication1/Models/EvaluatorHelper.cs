namespace WebApplication1.Models
{
    public static class RuleEvaluator
    {
        public static string EvaluateBloodPressure(int systolic, int diastolic, BloodPressureGuideline guideline)
        {
            var results = new List<(string Category, bool Matches)>
            {
                ("seriousIssue", Matches(systolic, guideline.SeriousIssue.Systolic) || Matches(diastolic, guideline.SeriousIssue.Diastolic)),
                ("needsAttention", Matches(systolic, guideline.NeedsAttention.Systolic) && Matches(diastolic, guideline.NeedsAttention.Diastolic)),
                ("optimal", Matches(systolic, guideline.Optimal.Systolic) && Matches(diastolic, guideline.Optimal.Diastolic))
            };
            return results.FirstOrDefault(r => r.Matches).Category ?? "unknown";
        }
        public static string EvaluateNumeric(double value, ValueGuideline guideline)
        {
            if (Matches(value, guideline.SeriousIssue)) return "seriousIssue";
            if (Matches(value, guideline.NeedsAttention)) return "needsAttention";
            if (Matches(value, guideline.Optimal)) return "optimal";
            return "unknown";
        }

        public static bool Matches(double value, string rule)
        {
            rule = rule.Trim();

            if (rule.Contains("-"))
            {
                var parts = rule.Split('-');
                if (double.TryParse(parts[0], out var min) && double.TryParse(parts[1], out var max))
                    return value >= min && value <= max;
            }
            else if (rule.StartsWith(">="))
            {
                return value >= double.Parse(rule.Substring(2));
            }
            else if (rule.StartsWith("<="))
            {
                return value <= double.Parse(rule.Substring(2));
            }
            else if (rule.StartsWith(">"))
            {
                return value > double.Parse(rule.Substring(1));
            }
            else if (rule.StartsWith("<"))
            {
                return value < double.Parse(rule.Substring(1));
            }
            return false;
        }
        public static string EvaluateText(string input, TextGuideline guideline)
        {
            if (input == guideline.SeriousIssue) return "seriousIssue";
            if (input == guideline.NeedsAttention) return "needsAttention";
            if (input == guideline.Optimal) return "optimal";
            return "unknown";
        }
    }
}
