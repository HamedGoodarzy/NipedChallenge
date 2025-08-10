using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
    public class Questionnaire : BaseModel
    {
        public Guid ClientId { get; set; } 
        public Client Client { get; set; }
        public int ExerciseWeeklyMinutes { get; set; }
        public string SleepQuality { get; set; }
        public string StressLevels { get; set; }
        public string DietQuality { get; set; }

    }
    public class QuestionnaireConfiguration : EntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
        }
    }
}
