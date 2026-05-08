using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.Controllers
{
    public class AddActivities
    {
        public readonly AppDbContext _db;
        public AddActivities(AppDbContext db)
        {
            _db = db;
        }

        public AddActivities(AppDbContext db, Guid userId, Guid courseId, Guid typeId, string name, DateTime dueDate)
        {
            _db = db;
            var activity = new AcademicTask
            {
                TaskID = Guid.NewGuid(),
                UserID = userId,
                CourseID = courseId,
                Name = name,
                DueDate = dueDate,
                TypeID = typeId
            };
            _db.AcademicTasks.Add(activity);
            _db.SaveChanges();
        }
    }
}