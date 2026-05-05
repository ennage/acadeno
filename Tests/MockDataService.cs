using Acadeno.Backend.Tools;

namespace Acadeno.Tests
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