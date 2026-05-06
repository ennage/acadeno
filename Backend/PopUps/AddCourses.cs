using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.PopUps
{
    public class AddCourses
    {
        public readonly AppDbContext _db;
        public AddCourses(AppDbContext db)
        {
            _db = db;
        }

        public AddCourses(AppDbContext db, string userId, string termId, string professor, string courseCode, string name, ScheduleEntry scheduleEntry, ScheduleEntry Room )
        {
            _db = db;
            var course = new Course
            {
                CourseID = Guid.NewGuid().ToString(),
                UserID = userId,
                TermID = termId,
                Professor = professor,
                CourseCode = courseCode,
                ScheduleEntrys = new List<ScheduleEntry> { scheduleEntry },
                Room = scheduleEntry.Room,
                Name = name
            };
            _db.Courses.Add(course);
            _db.SaveChanges();
        }
    }
}