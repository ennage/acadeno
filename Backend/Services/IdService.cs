using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.Services
{
    public class IdService
    {
        private readonly AppDbContext _db;

        public IdService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            // This tells Entity Framework: "Find the first user whose UserID matches this id"
            return await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
        }
    }
}