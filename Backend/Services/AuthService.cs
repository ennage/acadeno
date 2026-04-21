using Acadeno.Backend.Tools;
using Acadeno.Backend.Models.Admin;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services;

public class AuthService
{
    private readonly AppDbContext _db;
    public User? CurrentUser { get; private set; }

    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> RegisterUser(string username, string password)
    {
        if (await _db.Users.AnyAsync(u => u.Name == username)) return false;

        var newUser = new User { Name = username, Password = password };
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> LoginUser(string username, string password)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Name == username && u.Password == password);

        if (user != null)
        {
            CurrentUser = user;
            return true;
        }

        return false;
    }
}