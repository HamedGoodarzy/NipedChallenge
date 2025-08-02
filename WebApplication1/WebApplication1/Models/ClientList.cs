namespace WebApplication1.Models
{
    public class ClientList
    {
        public List<Client> Clients { get; set; }
    }

    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public MedicalData MedicalData { get; set; }
    }

    public class MedicalData
    {
        public Bloodwork Bloodwork { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }

    public class Bloodwork
    {
        public Cholesterol Cholesterol { get; set; }
        public int BloodSugar { get; set; }
        public BloodPressure BloodPressure { get; set; }
    }

    public class Cholesterol
    {
        public int Total { get; set; }
        public int Hdl { get; set; }
        public int Ldl { get; set; }
    }

    public class BloodPressure
    {
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }

    public class Questionnaire
    {
        public int ExerciseWeeklyMinutes { get; set; }
        public string SleepQuality { get; set; }
        public string StressLevels { get; set; }
        public string DietQuality { get; set; }
    }

}
