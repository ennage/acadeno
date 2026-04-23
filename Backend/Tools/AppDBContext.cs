using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models.Admin;
using Acadeno.Backend.Models.Education;
using Acadeno.Backend.Models.Schedule;

namespace Acadeno.Backend.Tools
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<AcademicTask> AcademicTasks { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<BaseTask> Tasks { get; set; }
        public DbSet<AcademicTaskType> AcademicTaskTypes { get; set; }
        public DbSet<ScheduleEntry> ScheduleEntries { get; set; }
    }
}