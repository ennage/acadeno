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

        public void AddNewCourse(Guid userId, Guid termId, string courseCode, string name, ScheduleEntry scheduleEntry)
        {
            var course = new Course
            {
                CourseID = Guid.NewGuid(),
                UserID = userId,
                TermID = termId,
                CourseCode = courseCode,
                Name = name,
                Room = scheduleEntry.Room,
                ScheduleEntrys = new List<ScheduleEntry> { scheduleEntry }
            };
            _db.Courses.Add(course);
            _db.SaveChanges();
        }
    }
}