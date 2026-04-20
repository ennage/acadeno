using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services;

public class AuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> RegisterUser(string username, string password)
    {
        // OOP Check: Does the user already exist?
        if (await _db.Users.AnyAsync(u => u.Name == username)) return false;

        var newUser = new User { Name = username, Password = password };
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return true;
    }
}