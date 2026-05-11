using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acadeno.Backend.Services
{
    public class CourseService
    {
        private readonly AppDbContext _db;

        public CourseService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Course> AddNewCourseAsync(string userId, string termId, string courseCode, string name, ScheduleEntry scheduleEntry)
        {
            var course = new Course
            {
                CourseID = Guid.NewGuid().ToString(),
                UserID = userId,
                TermID = termId,
                CourseCode = courseCode,
                Name = name,
                ScheduleEntries = new List<ScheduleEntry> { scheduleEntry }
            };

            scheduleEntry.CourseID = course.CourseID;
            
            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();

            return course;
        }

        public async Task<List<Course>>GetStudentCoursesAsync(string userId)
        {
            return await _db.Courses
                .AsNoTracking()
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }

        public async Task<object>GetCousrsesWithGrades(string userId)
        {
            var coursesWithGrades = _db.Courses
                .Where(c => c.UserID == userId)
                .Select(c => new
                {
                    c.CourseID,
                    c.Name,
                    Schedule = c.ScheduleEntries,
                    
                    CurrentGrade = c.ActualGrade != null ? c.ActualGrade.CourseGrade : 0,
                    Term = c.TermID
                })
                .ToList();

            return coursesWithGrades;
        }    
        public async Task<List<Course>> GetUserCoursesAsync(string userId)
        {
            return await _db.Courses
                .AsNoTracking()
                .Where(c => c.UserID == userId)
                .ToListAsync();
        }

    }
}