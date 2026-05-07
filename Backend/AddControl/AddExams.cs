using Acadeno.Backend.Enums;
using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.AddControl
{
    public class AddExams
    {
            public readonly AppDbContext _db;
            public AddExams(AppDbContext db)
            {
                _db = db;
            }
    
            public AddExams(AppDbContext db, string userId, string courseId, string name, DateTime date, string description, Status status)
            {
                _db = db;
                var exam = new AcademicTask
                {
                    TaskID = Guid.NewGuid().ToString(),
                    UserID = userId,
                    CourseID = courseId,
                    Name = name,
                    DueDate = date,
                    Description = description,
                    TaskStatus = status,
                    TypeID = "Exam"
                };
                _db.AcademicTasks.Add(exam);
                _db.SaveChanges();
            }
    }
}