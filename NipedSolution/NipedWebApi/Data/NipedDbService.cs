namespace NipedWebApi.Data
{
    public class NipedDbService (NipedDbContext db): INipedDbService
    {
        public NipedDbContext DbContext()
        {
            return db;
        }
    }

    public interface INipedDbService
    {
        NipedDbContext DbContext();
    }
}
