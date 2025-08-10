namespace NipedWebApi.Data
{
    public class NipedDbService : INipedDbService
    {
        private readonly NipedDbContext _db;
        public NipedDbService(NipedDbContext db)
        {
            _db = db;
        }

        public NipedDbContext DbContext()
        {
            return _db;
        }
    }

    public interface INipedDbService
    {
        NipedDbContext DbContext();
    }
}
