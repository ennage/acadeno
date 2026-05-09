using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class ViewAllCourses
    {
        public readonly AppDbContext _db;

        public ViewAllCourses(AppDbContext db)
        {
            _db = db;
        }

        public List<Course> GetAllCourses()
        {
            return _db.Courses.ToList();
        }

        public List<Course> GetStudentCourses(string userId)
        {
            return _db.Courses
            .Where(c => c.UserID == userId)
            .ToList();
        }

        public object GetCoursesWithGrades(string userId)
        {
            var coursesWithGrades = _db.Courses
                .Where(c => c.UserID == userId)
                .Select(c => new
                {
                    c.CourseID,
                    c.Name,
                    Schedule = c.ScheduleEntrys,
                    Room = c.Room,
                    
                    CurrentGrade = c.ActualGrade != null ? c.ActualGrade.CourseGrade : 0,
                    Term = c.TermID
                })
                .ToList();

            return coursesWithGrades;
        }

    }
}