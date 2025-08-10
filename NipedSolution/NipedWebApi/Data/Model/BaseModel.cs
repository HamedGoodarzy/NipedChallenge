using System.ComponentModel.DataAnnotations;

namespace NipedWebApi.Data.Model
{
    public class BaseModel
    {
        public BaseModel()
        {
            Updated = DateTime.UtcNow;
            Created = Created ?? Updated;
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; } = [];
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
