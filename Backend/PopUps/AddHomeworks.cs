using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;

namespace Acadeno.Backend.PopUps
{
    public class AddHomeworks
    {
        public readonly AppDbContext _db;
        public AddHomeworks(AppDbContext db)
        {
            _db = db;
        }

        public AddHomeworks(AppDbContext db, string title, string userId, string courseId, string name, DateTime dueDate)
        {
            _db = db;
            var homework = new AcademicTask
            {
                TaskID = Guid.NewGuid().ToString(),
                UserID = userId,
                CourseID = courseId,
                Name = name,
                DueDate = dueDate,
                TypeID = "Homework"
            };
            _db.AcademicTasks.Add(homework);
            _db.SaveChanges();
        }
    }
}