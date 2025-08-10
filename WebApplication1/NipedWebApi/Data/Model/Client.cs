using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
    public class Client : BaseModel
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public virtual Bloodwork? Bloodwork { get; set; }
        public virtual Questionnaire? Questionnaire { get; set; }
    }

    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasOne(e => e.Bloodwork)
            .WithOne(e => e.Client)
            .HasForeignKey<Bloodwork>(e => e.ClientId)
            .IsRequired();

            builder.HasOne(e => e.Questionnaire)
            .WithOne(e => e.Client)
            .HasForeignKey<Questionnaire>(e => e.ClientId)
            .IsRequired();
        }
    }

    public class Bloodwork : BaseModel
    {
        public Guid ClientId { get; set; } 
        public Client Client { get; set; } 
        public int CholesterolTotal { get; set; }
        public int CholesterolHdl { get; set; }
        public int CholesterolLdl { get; set; }
        public int BloodSugar { get; set; }
        public int BloodPressureSystolic { get; set; }
        public int BloodPressureDiastolic { get; set; }

    }
    public class BloodworkConfiguration : EntityTypeConfiguration<Bloodwork>
    {
        public void Configure(EntityTypeBuilder<Bloodwork> builder)
        {
        }
    }

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
