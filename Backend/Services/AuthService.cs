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
        public User? CurrentUser {get; private set;}

        public AuthService(AppDbContext db)
        {
            _db = db;
        }

        private bool IsDBLocked(Exception ex)
        {
            return ex is Microsoft.Data.Sqlite.SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 5;
        }
        private async Task<bool> SaveChangesSafelyAsync()
        {
            int retryCount = 0;
            while (retryCount < 3)
            {
                try
                {
                    await _db.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex) when (IsDBLocked(ex))
                {
                    retryCount++;
                    await Task.Delay(500);
                    System.Diagnostics.Debug.WriteLine("Database is locked. Please try again.");
                }
                catch (Exception ex) when (IsDBFull(ex))
                {
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error saving changes: {ex.Message}");
                    return false;
                }
            }
            return false;
        }

        private bool IsDBFull(Exception ex)
        {
            return ex.HResult == unchecked((int)0x80070070) || 
                     ex.Message.Contains("There is not enough space on the Database", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> RegisterUser(User newUser, string password)
        {
            string cleanEmail = newUser.Email.Trim().ToLower();
            bool exists = await _db.Users.AnyAsync(u => u.Email == cleanEmail);
            if (exists) return false;

            newUser.Email = cleanEmail;
            newUser.Password = HashPassword(password);

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User?> LoginUser(string email, string password)
        {
            string cleanEmail = email.Trim().ToLower();     
            string hashedInput = HashPassword(password);    

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

        public async Task<bool> LogoutUser(bool clearRememberMe = false)
        {
            try
            {
                CurrentUser = null;
            
                if (clearRememberMe)
                {
                    
                }
                
                return await SaveChangesSafelyAsync();
            }
            catch
            {
                return false;
            }
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