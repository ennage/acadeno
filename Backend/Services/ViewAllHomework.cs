using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class ViewAllHomework
    {
        public readonly AppDbContext _db;
        public ViewAllHomework(AppDbContext db)
        {
            _db = db;
        }
         public List<AcademicTask> GetAllHomework()
        {
        return _db.AcademicTasks
        .Where(t => t.TypeID == "Homework")
        .ToList();
        }
    }
}