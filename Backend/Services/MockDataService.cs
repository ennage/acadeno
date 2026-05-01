using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class MockDataService
    {
        private readonly AppDbContext _db;

        public MockDataService(AppDbContext db)
        {
            _db = db;
        }
    }
}    