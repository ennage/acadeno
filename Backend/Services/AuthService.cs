using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        private readonly IdService _idService;
        public User? CurrentUser {get; private set;}

        public AuthService(AppDbContext db, IdService idService)
        {
            _db = db;
            _idService = idService;
        }

        public async Task<bool> RegisterUser(User newUser, string password)
        {
            string cleanEmail = newUser.Email.Trim().ToLower();
            bool exists = await _db.Users.AnyAsync(u => u.Email == cleanEmail);
            if (exists) return false; // account exists, don't create

            newUser.Email = cleanEmail;
            newUser.UserID = await _idService.GenerateNextUserID();
            newUser.Password = HashPassword(password);

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> LoginUser(string email, string password)
        {
            string cleanEmail = email.Trim().ToLower();     // removes accidental spaces
            string hashedInput = HashPassword(password);    // encrypted password

            System.Diagnostics.Debug.WriteLine($"DEBUG: Email: {email}");
            System.Diagnostics.Debug.WriteLine($"DEBUG: Computed Hash: {hashedInput}");

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedInput);

            if (user != null)
            {
                CurrentUser = user;
                return user;
            }
            return null;
        }

        public async Task<bool> UpdateProfile(User updatedUser)
        {
            var user = await _db.Users.FindAsync(updatedUser.UserID);
            if (user == null) return false;
            user.University = updatedUser.University;

            await _db.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}