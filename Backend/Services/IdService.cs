using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class IdService
    {
        private readonly AppDbContext _db;

        public IdService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerateNextUserID()
        {
            //  get all users, find the max ID
            var lastUser = await _db.Users
                .OrderByDescending(u => u.UserID)
                .FirstOrDefaultAsync();

            if (lastUser == null) return "U0001";

            //  extract the number (e.g., "U0001" -> 1)
            string numericPart = lastUser.UserID.Substring(1);
            if (int.TryParse(numericPart, out int lastId))
            {
                int nextId = lastId + 1;
                return $"U{nextId:D4}"; // "D4" ensures it stays 4 digits (0002)
            }
            return "U0001"; 
        }
    }
}