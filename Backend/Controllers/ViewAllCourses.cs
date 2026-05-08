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

        public List<Course> GetStudentCourses(Guid userId)
        {
            return _db.Courses
            .Where(c => c.UserID == userId)
            .ToList();
        }

    }
}