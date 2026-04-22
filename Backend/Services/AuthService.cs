using Acadeno.Backend.Tools;
using Acadeno.Backend.Models.Education;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Acadeno.Backend.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    public User? CurrentUser { get; private set; }

    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> RegisterUser(User newUser, string password)
    {
        if (await _db.Users.AnyAsync(u => u.UserID == newUser.UserID)) return false;

        newUser.Password = HashPassword(password);
       
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<User?> LoginUser(string username, string password)
    {
        string hashedInput =HashPassword(password);
        
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Name == username && u.Password == hashedInput);

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
        user.TargetGenAve = updatedUser.TargetGenAve;

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