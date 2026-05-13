using Acadeno.Backend.DTOs;
using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class CourseService
    {
        private readonly AppDbContext _db;

        public CourseService(AppDbContext db)
        {
            _db = db;
        }

        // Standardized Course Creation (Ensures Simulator Works!)
        public async Task<Course> AddNewCourseAsync(string userId, string termId, string courseCode, string name, int units)
        {
            var course = new Course {
                CourseID = Guid.NewGuid().ToString(),
                UserID = userId,
                TermID = termId,
                CourseCode = courseCode,
                Name = name,
                Units = units,
                ActualGrade = new Grade { GradeID = Guid.NewGuid().ToString() }
            };

            // Fetch Global Definitions
            var definitions = await _db.TaskTypeDefinitions
                .Where(d => d.Name != "Unallocated")
                .ToListAsync();

            // Auto-generate the "Category Buckets"
            foreach (var def in definitions)
            {
                course.ActualGrade.AcademicTaskTypes.Add(new AcademicTaskType {
                    TypeID = Guid.NewGuid().ToString(),
                    DefID = def.DefID,
                    Name = def.Name,
                    Weight = 0 
                });
            }

            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();
            return course;
        }

        // Standardized Schedule-based Creation
        public async Task AddCourseWithSchedulesAsync(string userId, string termId, string courseCode, string name, int units, List<ScheduleEntry> schedules)
        {
            // Use existing logic to create the course and category buckets
            var course = await AddNewCourseAsync(userId, termId, courseCode, name, units);

            foreach (var entry in schedules)
            {
                entry.CourseID = course.CourseID;
                _db.ScheduleEntries.Add(entry);
            }
            
            await _db.SaveChangesAsync();
        }

        public async Task<List<Course>> GetUserCoursesAsync(string userId)
        {
            return await _db.Courses
                .AsNoTracking()
                .Include(c => c.ActualGrade)
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }

        // Cleaned up naming and return type
        public async Task<List<CourseDashboard>> GetCoursesForDashboardAsync(string userId)
        {
            return await _db.Courses
                .Where(c => c.UserID == userId)
                .Select(c => new CourseDashboard
                {
                    CourseID = c.CourseID,
                    Name = c.Name,
                    CourseCode = c.CourseCode,
                    CurrentGrade = c.ActualGrade != null ? (c.ActualGrade.CourseGrade ?? 0) : 0,
                    TermID = c.TermID
                })
                .ToListAsync();
        }

        public async Task<List<TaskTypeDefinition>> GetAllTaskTypeDefinitionsAsync()
        {
            return await _db.TaskTypeDefinitions.AsNoTracking().ToListAsync();
        }

        public async Task<Course?> GetCourseWithFullDetailsAsync(string courseId)
        {
            return await _db.Courses
                .AsNoTracking()
                .Include(c => c.ActualGrade)
                    .ThenInclude(g => g.AcademicTaskTypes)
                        .ThenInclude(type => type.Definition)
                .Include(c => c.ActualGrade)
                    .ThenInclude(g => g.AcademicTaskTypes)
                        .ThenInclude(type => type.AcademicTasks)
                .FirstOrDefaultAsync(c => c.CourseID == courseId);
        }

        public async Task<bool> SaveOfficialSyllabusWeightsAsync(string gradeId, List<AcademicTaskType> simulatedCategories)
        {
            var actualGrade = await _db.Grades
                .Include(g => g.AcademicTaskTypes)
                .FirstOrDefaultAsync(g => g.GradeID == gradeId);

            if (actualGrade == null) return false;

            foreach (var simCat in simulatedCategories)
            {
                // Find the matching category in the actual database using the Definition ID
                var existingCat = actualGrade.AcademicTaskTypes.FirstOrDefault(c => c.DefID == simCat.DefID);
                
                if (existingCat != null)
                {
                    // Update the existing weight
                    existingCat.Weight = simCat.Weight;
                    _db.AcademicTaskTypes.Update(existingCat);
                }
                else
                {
                    // If the user injected a BRAND NEW category in the simulator, add it to the DB!
                    var newCat = new AcademicTaskType
                    {
                        TypeID = Guid.NewGuid().ToString(),
                        GradeID = actualGrade.GradeID,
                        DefID = simCat.DefID,
                        Name = simCat.Name,
                        Weight = simCat.Weight
                    };
                    await _db.AcademicTaskTypes.AddAsync(newCat);
                }
            }

            return await _db.SaveChangesAsync() > 0;
        }

        public Course DeepCloneCourse(Course original)
        {
            if (original == null) return new Course();

            return new Course
            {
                CourseID = Guid.NewGuid().ToString(),
                Name = original.Name,
                CourseCode = original.CourseCode,
                Units = original.Units,
                ActualGrade = new Grade
                {
                    AcademicTaskTypes = original.ActualGrade?.AcademicTaskTypes?.Select(type => new AcademicTaskType
                    {
                        TypeID = Guid.NewGuid().ToString(),
                        Name = type.Name,
                        Weight = type.Weight,
                        DefID = type.DefID,
                        Definition = type.Definition,
                        AcademicTasks = type.AcademicTasks?
                            .Where(task => task.CourseID == original.CourseID)
                            .Select(task => new AcademicTask
                            {
                                TaskID = Guid.NewGuid().ToString(),
                                Name = task.Name,
                                Score = task.Score,
                                MaxScore = task.MaxScore
                            }).ToList() ?? new List<AcademicTask>()
                    }).ToList() ?? new List<AcademicTaskType>()
                }
            };
        }
    }
}