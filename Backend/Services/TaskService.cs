using Acadeno.Backend.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<AcademicTask>> GetAllTasks(string userId)
        {
            return await _db.AcademicTasks
                .Where(t => t.UserID == userId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<List<AcademicTask>> GetAcademicTasks(string UserId)
        {
            return await _db.AcademicTasks
                .Where(t => t.UserID == UserId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<bool> CreateTask(AcademicTask task)
        {
            if (task == null) return false;

            _db.AcademicTasks.Add(task);
            await _db.SaveChangesAsync();   
            return true;
        }

        public int CalculateRiskLevel(string taskid)
        {
            var task = _db.AcademicTasks.Find(taskid);
            if (task == null || task.DueDate == null) return 1;

           int score = 1;
           var timeReamaining = (DateTime)task.DueDate - DateTime.Now;

           if (timeReamaining.TotalDays <= 1) score += 3;
           else if (timeReamaining.TotalDays <= 3) score += 2;
           else if (timeReamaining.TotalDays <= 7) score += 1;

           return Math.Clamp(score, 1, 5);
        }
    }
}