using WebApplication1.Domain;
using WebApplication1.Models;

namespace WebApplication1.Utilities
{
    public class ReportGenerator
    {
        private readonly GuidelineSet _guidelines;

        public ReportGenerator(GuidelineSet guidelines)
        {
            _guidelines = guidelines;
        }

        public List<ReportEntry> GenerateReport(Dictionary<string, object> flatData)
        {
            var report = new List<ReportEntry>();

            foreach (var kvp in flatData)
            {
                var metric = kvp.Key;
                var value = kvp.Value;

                // Match guideline by metric path
                switch (metric)
                {
                    //TODO Hamed
                    //case "bloodwork.cholesterol.total":
                        //report.Add(EvaluateNumeric(metric, value, _guidelines.Cholesterol["total"].Total)); break;
                    //case "bloodwork.cholesterol.hdl":
                        //report.Add(EvaluateNumeric(metric, value, _guidelines.Cholesterol["hdl"].Hdl)); break;
                    //case "bloodwork.cholesterol.ldl":
                        //report.Add(EvaluateNumeric(metric, value, _guidelines.Cholesterol["ldl"].Ldl)); break;
                    case "Bloodwork.BloodSugar":
                        report.Add(EvaluateNumeric(metric, value, _guidelines.BloodSugar)); break;
                    case "Questionnaire.ExerciseWeeklyMinutes":
                        report.Add(EvaluateNumeric(metric, value, _guidelines.ExerciseWeeklyMinutes)); break;
                    case "Questionnaire.SleepQuality":
                        report.Add(EvaluateText(metric, value, _guidelines.SleepQuality)); break;
                    case "Questionnaire.StressLevels":
                        report.Add(EvaluateText(metric, value, _guidelines.StressLevels)); break;
                    case "Questionnaire.DietQuality":
                        report.Add(EvaluateText(metric, value, _guidelines.DietQuality)); break;
                    case "Bloodwork.BloodPressure.systolic":
                    case "Bloodwork.BloodPressure.diastolic":
                        // special case handled outside since systolic+diastolic go together
                        break;
                }
            }

            // Handle blood pressure together
            if (flatData.TryGetValue("bloodwork.bloodPressure.systolic", out var systolicObj) &&
                flatData.TryGetValue("bloodwork.bloodPressure.diastolic", out var diastolicObj))
            {
                var systolic = Convert.ToInt32(systolicObj);
                var diastolic = Convert.ToInt32(diastolicObj);
                var category = RuleEvaluator.EvaluateBloodPressure(systolic, diastolic, _guidelines.BloodPressure);

                report.Add(new ReportEntry
                {
                    MetricPath = "bloodwork.bloodPressure",
                    Value = $"{systolic}/{diastolic}",
                    Category = category,
                    Explanation = $"Systolic: {systolic}, Diastolic: {diastolic}"
                });
            }

            return report;
        }

        private ReportEntry EvaluateNumeric(string path, object value, ValueGuideline guideline)
        {
            var numeric = Convert.ToDouble(value);
            var category = RuleEvaluator.EvaluateNumeric(numeric, guideline);
            return new ReportEntry
            {
                MetricPath = path,
                Value = value,
                Category = category,
                Explanation = $"{numeric} → {category}"
            };
        }
        private ReportEntry EvaluateText(string path, object value, TextGuideline guideline)
        {
            var str = value.ToString();
            var category = RuleEvaluator.EvaluateText(str, guideline);
            return new ReportEntry
            {
                MetricPath = path,
                Value = str,
                Category = category,
                Explanation = $"{str} → {category}"
            };
        }
    }
}
