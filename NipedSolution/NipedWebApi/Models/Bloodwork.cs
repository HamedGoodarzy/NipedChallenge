using NipedWebApi.Data.Model;

namespace WebApplication1.Models
{
    public class Bloodwork : BaseModel
    {
        public Cholesterol Cholesterol { get; set; }
        public int BloodSugar { get; set; }
        public BloodPressure BloodPressure { get; set; }
    }
}
