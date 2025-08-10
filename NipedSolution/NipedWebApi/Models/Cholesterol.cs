using NipedWebApi.Data.Model;

namespace WebApplication1.Models
{
    public class Cholesterol: BaseModel
    {
        public int Total { get; set; }
        public int Hdl { get; set; }
        public int Ldl { get; set; }

    }
}
