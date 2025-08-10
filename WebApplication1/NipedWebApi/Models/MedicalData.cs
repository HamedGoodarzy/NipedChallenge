using NipedWebApi.Data.Model;

namespace WebApplication1.Models
{
    public class MedicalData : BaseModel
    {
        public Bloodwork Bloodwork { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}