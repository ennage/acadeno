using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class ViewAllExams
    {
        public readonly AppDbContext _db;
        public ViewAllExams(AppDbContext db)
        {
            _db = db;
        }
        public List<AcademicTask> GetAllExams(Guid typeId)
        {
        return _db.AcademicTasks
        .Include(t => t.Type)
        .Where(t => t.TypeID == typeId)
        .ToList();
        }
    }
}