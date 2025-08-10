using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;

namespace NipedWebApi.Data.Model
{
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
}
