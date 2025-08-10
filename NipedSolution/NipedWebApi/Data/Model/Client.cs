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
}
