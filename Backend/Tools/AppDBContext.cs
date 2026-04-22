using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models.Admin;
using Acadeno.Backend.Models.Schedule;
using Acadeno.Backend.Models.Education;
namespace Acadeno.Backend.Tools
{
    public class AppDbContext : DbContext
    {
        //  Constructor that accepts settings from MauiProgram.cs
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated(); // This ensures the file and tables are created
        }
        public DbSet<User> Users {get; set;}
        public DbSet<AcademicYear> AcademicYears {get; set;}
        public DbSet<AcademicTask> AcademicTasks {get; set;}
        public DbSet<Term> Terms {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<Grade> Grades {get; set;}
        public DbSet<AcademicTaskType> AcademicTaskTypes {get; set;}
        public DbSet<BaseTask> Tasks {get; set;}
        public DbSet<ScheduleEntry> ScheduleEntries {get; set;}
    }
}