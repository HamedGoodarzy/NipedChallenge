using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
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
