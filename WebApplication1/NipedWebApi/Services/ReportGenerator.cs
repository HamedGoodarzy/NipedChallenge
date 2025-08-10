using NipedModel;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ReportGenerator (GuidelineTO guideline, IRuleEvaluator ruleEvaluator) : IReportGenerator
    {
        public List<ClientReportEntryTO> GenerateReportEntries(ClientTO clientTO)
        {
            var reportEnttries = new List<ClientReportEntryTO>();
            reportEnttries.Add(EvaluateNumeric("Bloodwork.Cholesterol.Total", clientTO.MedicalData.Bloodwork.Cholesterol.Total, guideline.Cholesterol.Total));
            ////report.Add
            //{
                //EvaluateNumeric("Bloodwork.Cholesterol.Total", clientTO.MedicalData.Bloodwork.Cholesterol.Total, guideline.Cholesterol.Total),
                //EvaluateNumeric("Bloodwork.Cholesterol.Hdl", clientTO.MedicalData.Bloodwork.Cholesterol.Hdl, guideline.Cholesterol.Hdl),
                //EvaluateNumeric("Bloodwork.Cholesterol.Ldl", clientTO.MedicalData.Bloodwork.Cholesterol.Ldl, guideline.Cholesterol.Ldl),
            //};

            //foreach (var kvp in flatData)
            //{
            //    var metric = kvp.Key;
            //    var value = kvp.Value;
            //
            //    // Match guideline by metric path
            //    switch (metric)
            //    {
            //        //TODO Hamed
            //        case "Bloodwork.Cholesterol.Total":
            //            report.Add(EvaluateNumeric(metric, value, guideline.Cholesterol.Total)); break;
            //        case "Bloodwork.Cholesterol.Hdl":
            //            report.Add(EvaluateNumeric(metric, value, guideline.Cholesterol.Hdl)); break;
            //        case "Bloodwork.Cholesterol.Ldl":
            //            report.Add(EvaluateNumeric(metric, value, guideline.Cholesterol.Ldl)); break;
            //        case "Bloodwork.BloodSugar":
            //            report.Add(EvaluateNumeric(metric, value, guideline.BloodSugar)); break;
            //        case "Questionnaire.ExerciseWeeklyMinutes":
            //            report.Add(EvaluateNumeric(metric, value, guideline.ExerciseWeeklyMinutes)); break;
            //        case "Questionnaire.SleepQuality":
            //            report.Add(EvaluateText(metric, value, guideline.SleepQuality)); break;
            //        case "Questionnaire.StressLevels":
            //            report.Add(EvaluateText(metric, value, guideline.StressLevels)); break;
            //        case "Questionnaire.DietQuality":
            //            report.Add(EvaluateText(metric, value, guideline.DietQuality)); break;
            //        case "Bloodwork.BloodPressure.systolic":
            //        //report.Add(EvaluateText(metric,value,guideline.BloodPressure.
            //        case "Bloodwork.BloodPressure.diastolic":
            //            // special case handled outside since systolic+diastolic go together
            //            break;
            //    }
            //}

            // Handle blood pressure together
            //if (flatData.TryGetValue("Bloodwork.BloodPressure.Systolic", out var systolicObj) &&
            //    flatData.TryGetValue("Bloodwork.BloodPressure.Diastolic", out var diastolicObj))
            //{
            //    var systolic = Convert.ToInt32(systolicObj);
            //    var diastolic = Convert.ToInt32(diastolicObj);
            //    var category = _ruleEvaluator.EvaluateBloodPressure(systolic, diastolic, guideline.BloodPressure);
            //
            //    report.Add(new ReportEntry
            //    {
            //        MetricPath = "Bloodwork.BloodPressure",
            //        Value = $"{systolic}/{diastolic}",
            //        Category = category,
            //        Explanation = $"Systolic: {systolic}, Diastolic: {diastolic}"
            //    });
            //}

            return reportEnttries;
        }

        private ClientReportEntryTO EvaluateNumeric(string path, object value, ValueGuidelineTO guideline)
        {
            var numeric = Convert.ToDouble(value);
            var category = ruleEvaluator.EvaluateNumeric(numeric, guideline);
            return new ClientReportEntryTO
            {
                MetricPath = path,
                Value = value,
                Category = category,
                Explanation = $"{numeric} -> {category}"
            };
        }
        //private ReportEntry EvaluateText(string path, object value, TextGuideline guideline)
        //{
        //    var str = value.ToString();
        //    var category = _ruleEvaluator.EvaluateText(str, guideline);
        //    return new ReportEntry
        //    {
        //        MetricPath = path,
        //        Value = str,
        //        Category = category,
        //        Explanation = $"{str} -> {category}"
        //    };
        //}
    }

    interface IReportGenerator
    {
        List<ClientReportEntryTO> GenerateReportEntries(ClientTO clientTO);
    }

}
