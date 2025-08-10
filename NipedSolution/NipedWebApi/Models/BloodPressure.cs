using NipedWebApi.Data.Model;

namespace WebApplication1.Models
{
    public class BloodPressure : BaseModel
    {
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }
}
