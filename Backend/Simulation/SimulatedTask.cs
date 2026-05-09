using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Controllers
{
    public class SimulatedTask
    {
        private readonly AppDbContext _db;

        public SimulatedTask(AppDbContext db)
        {
            _db = db;
        }

        public void AddNewSimulatedTask(string userId, string courseId, string typeId, string name, Status status)
        {
            var simulatedTask = new AcademicTask
            {
                TaskID = Guid.NewGuid().ToString(),
                UserID = userId.ToString(),
                CourseID = courseId,
                TypeID = typeId,
                Name = name,
                TaskStatus = status,

                Score = 0, 
                MaxScore = 100
            };

            
            _db.Tasks.Add(simulatedTask);
            _db.SaveChanges();
        }
    }
}