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
        
        public DbSet<User> Users { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<AcademicTask> AcademicTasks { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<BaseTask> Tasks { get; set; }
        public DbSet<AcademicTaskType> AcademicTaskTypes { get; set; }
        public DbSet<ScheduleEntry> ScheduleEntries { get; set; }
        public DbSet<CalendarEntry> CalendarEntries { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Maps both BaseTask and AcademicTask to the 'tasks' table
            modelBuilder.Entity<BaseTask>()
                .ToTable("tasks")
                .HasDiscriminator<int>("IsAcademic") 
                .HasValue<BaseTask>(0)      // Normal task
                .HasValue<AcademicTask>(1); // Academic task

            modelBuilder.Entity<BaseTask>()
                .Property(t => t.TaskStatus)
                .HasColumnName("Status"); // Matches your SQL 'Status' column

            // Explicit Lowercase Table Mappings
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<AcademicYear>().ToTable("academicyears");
            modelBuilder.Entity<Term>().ToTable("terms");
            modelBuilder.Entity<Course>().ToTable("courses");
            modelBuilder.Entity<Grade>().ToTable("grades");
            modelBuilder.Entity<AcademicTaskType>().ToTable("academictasktypes");
            modelBuilder.Entity<ScheduleEntry>().ToTable("scheduleentries");
            modelBuilder.Entity<CalendarEntry>().ToTable("calendarentries");

            // Specific Column Mappings for CalendarEntry
            modelBuilder.Entity<CalendarEntry>(entity =>
            {
                entity.Property(e => e.Year).HasColumnName("EntryYear");
                entity.Property(e => e.Month).HasColumnName("EntryMonth");
                entity.Property(e => e.Day).HasColumnName("EntryDay");
            });
        }
    }
}