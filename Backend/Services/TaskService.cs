using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models.Education;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class TaskService
    {
        private readonly AppDbContext _db;

        public TaskService(AppDbContext db)
        {
            _db = db;
        }

        public async System.Threading.Tasks.Task<List<AcademicTask>> GetAllTasks(string userId)
        {
            return await _db.AcademicTasks
                .Where(t => t.UserID == userId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<List<AcademicTask>> GetAcademicTasks(string UserId)
        {
            return await _db.AcademicTasks
                .Where(t => t.UserID == UserId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<bool> CreateTask(AcademicTask task)
        {
            if (task == null) return false;

            _db.AcademicTasks.Add(task);
            await _db.SaveChangesAsync();   
            return true;
        }

        public int CalculateRiskLevel(string taskid)
        {
            var task = _db.AcademicTasks.Find(taskid);
            if (task == null) return 1;

           int score = 1;
           var timeReamaining = (DateTime)task.DueDate - DateTime.Now;

           if (timeReamaining.TotalDays <= 1) score += 3;
           else if (timeReamaining.TotalDays <= 3) score += 2;
           else if (timeReamaining.TotalDays <= 7) score += 1;

           if (task.RiskLevel == Models.Enums.RiskLevel.Critical) score += 5;
           else if (task.RiskLevel == Models.Enums.RiskLevel.Warning) score += 3;
              else if (task.RiskLevel == Models.Enums.RiskLevel.Stable) score += 1;

           return Math.Clamp(score, 1, 5);
        }
    }
}