using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class ViewAllActivities
    {
        public readonly AppDbContext _db;
        public ViewAllActivities(AppDbContext db)
        {
            _db = db;
        }
        public List<AcademicTask> GetAllActivities(string typeId)
        {
        return _db.AcademicTasks
        .Where(t => t.TypeID == typeId)
        .ToList();
        }
    }
}