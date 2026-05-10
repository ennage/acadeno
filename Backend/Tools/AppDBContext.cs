using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.Tools
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users {get; set;}
        public DbSet<AcademicYear> AcademicYears {get; set;}
        public DbSet<AcademicTask> AcademicTasks {get; set;}
        public DbSet<Term> Terms {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<Grade> Grades {get; set;}
        public DbSet<BaseTask> Tasks {get; set;}
        public DbSet<AcademicTaskType> AcademicTaskTypes {get; set;}
        public DbSet<TaskTypeDefinition> TaskTypeDefinitions {get; set;}
        public DbSet<ScheduleEntry> ScheduleEntries {get; set;}
        public DbSet<CalendarEntry> CalendarEntries {get; set;} 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- USERS ---
            modelBuilder.Entity<User>(entity => {
                entity.ToTable("users");
                entity.HasKey(u => u.UserID); 
            });

            // --- TASK HIERARCHY (Table-Per-Hierarchy) ---
            modelBuilder.Entity<BaseTask>()
                .ToTable("tasks") 
                .HasDiscriminator<int>("IsAcademic") 
                .HasValue<BaseTask>(0)  
                .HasValue<AcademicTask>(1); 

            modelBuilder.Entity<BaseTask>(entity => {
                entity.HasKey(t => t.TaskID);
                entity.Property(t => t.TaskStatus).HasColumnName("Status");
                entity.Property(t => t.StartDate).HasColumnName("StartDate");
                entity.Property(t => t.DueDate).HasColumnName("DueDate");
                // Explicitly map UserID as a string to ensure no Guid conversion is attempted
                entity.Property(t => t.UserID).IsRequired();
            });

            // --- SCHEDULE ENTRIES ---
            modelBuilder.Entity<ScheduleEntry>(entity => {
                entity.ToTable("scheduleentries");
                entity.HasKey(e => e.EntryID);
                entity.Property(e => e.StartTime).HasColumnName("StartTime");
                entity.Property(e => e.EndTime).HasColumnName("EndTime");
            });

            // --- CALENDAR ENTRIES ---
            modelBuilder.Entity<CalendarEntry>(entity => {
                entity.ToTable("calendarentries");
                entity.Property(e => e.Year).HasColumnName("EntryYear");
                entity.Property(e => e.Month).HasColumnName("EntryMonth");
                entity.Property(e => e.Day).HasColumnName("EntryDay");
            });

            // --- REMAINING TABLES ---
            modelBuilder.Entity<Course>(entity => {
                entity.ToTable("courses");
                entity.HasKey(c => c.CourseID);
            });
            
            modelBuilder.Entity<AcademicTaskType>().ToTable("academictasktypes");
            modelBuilder.Entity<TaskTypeDefinition>().ToTable("tasktypedefinitions");
            modelBuilder.Entity<AcademicYear>().ToTable("academicyears");
            modelBuilder.Entity<Term>().ToTable("terms");
            modelBuilder.Entity<Grade>().ToTable("grades");

            // // --- FLUENT API RELATIONSHIPS ---

            // // 1. Link the global Definition to the Course-Specific Task Types
            // modelBuilder.Entity<AcademicTaskType>()
            //     .HasOne(type => type.Definition)
            //     .WithMany(def => def.AcademicTaskTypes)
            //     .HasForeignKey(type => type.DefID);

            // // 2. Explicitly link Course to Grade (1-to-1)
            // modelBuilder.Entity<Course>()
            //     .HasOne(c => c.ActualGrade)
            //     .WithOne(g => g.Course)
            //     .HasForeignKey<Grade>(g => g.CourseID); 

            // // 3. Link Tasks to their TaskTypes
            // modelBuilder.Entity<AcademicTask>()
            //     .HasOne(t => t.Type)
            //     .WithMany(type => type.AcademicTasks)
            //     .HasForeignKey(t => t.TypeID);
        }
    }
}