using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.Tools
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }
        
        public DbSet<User> Users {get; set;}
        public DbSet<AcademicYear> AcademicYears {get; set;}
        public DbSet<AcademicTask> AcademicTasks {get; set;}
        public DbSet<Term> Terms {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<Grade> Grades {get; set;}
        public DbSet<BaseTask> Tasks {get; set;}
        public DbSet<AcademicTaskType> AcademicTaskTypes {get; set;}
        public DbSet<ScheduleEntry> ScheduleEntries {get; set;}
        public DbSet<Calendar> Calendars {get; set;} 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseTask>()
                .HasDiscriminator<int>("IsAcademic") // Use your existing column as the label
                .HasValue<BaseTask>(0)              // 0 means it's a normal task
                .HasValue<AcademicTask>(1);         // 1 means it's an AcademicTask
                
            // Also fix that naming mismatch from image_addb1a.png
            modelBuilder.Entity<BaseTask>()
                .Property(t => t.TaskStatus)
                .HasColumnName("Status");
        }
    }

    
}