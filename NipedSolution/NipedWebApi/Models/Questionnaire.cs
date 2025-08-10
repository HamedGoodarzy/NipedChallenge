using NipedWebApi.Data.Model;

namespace WebApplication1.Models
{
    public class Questionnaire : BaseModel
    {
        public int ExerciseWeeklyMinutes { get; set; }
        public string SleepQuality { get; set; }
        public string StressLevels { get; set; }
        public string DietQuality { get; set; }
    }

}
