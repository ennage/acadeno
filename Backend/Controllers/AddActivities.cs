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

        public AddActivities(AppDbContext db, string userId, string courseId, string typeId, string name, DateTime dueDate)
        {
            _db = db;
            var activity = new AcademicTask
            {
                TaskID = Guid.NewGuid().ToString(),
                UserID = userId.ToString(),
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