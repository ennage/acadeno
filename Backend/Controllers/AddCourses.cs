using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.Controllers
{
    public class AddCourses
    {
        private readonly AppDbContext _db;

        public AddCourses(AppDbContext db)
        {
            _db = db;
        }

        public void AddNewCourse(string userId, string termId, string courseCode, string name, ScheduleEntry scheduleEntry)
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
            
            _db.Courses.Add(course);
            _db.SaveChanges();
        }
    }
}