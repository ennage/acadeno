using Acadeno.Backend.Enums;
using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Controllers
{
    public class AddExams
    {
        private readonly AppDbContext _db;

        public AddExams(AppDbContext db)
        {
            _db = db;
        }

        public void AddNewExam(Guid userId, Guid courseId, Guid typeId, string name, DateTime date, string description, Status status)
        {
            var exam = new AcademicTask
            {
                TaskID = Guid.NewGuid(),
                UserID = userId,
                CourseID = courseId,
                TypeID = typeId,
                Name = name,
                DueDate = date,
                Description = description,
                TaskStatus = status
            };
            _db.Tasks.Add(exam);
            _db.SaveChanges();
        }
    }
}