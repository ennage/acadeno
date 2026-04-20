using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.Tools;

public class AppDbContext : DbContext
{
    // This creates the "Users" table in your SQLite file
    public DbSet<User> Users { get; set; }

    // This tells EF Core to use SQLite and where the file is
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // "Data Source" is the filename. 
        // It will look for acadeno.db in your project folder.
        options.UseSqlite("Data Source=acadeno.db");
    }
}