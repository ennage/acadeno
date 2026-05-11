using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;
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

        public async Task AddNewActivitiesAsync(string userId, string courseId, string typeId, string name, DateTime dueDate)
        {
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
            await _db.SaveChangesAsync();
        }

        public async Task AllExams(string userId, string courseId, string typeId, string name, DateTime date, Status status)
        {
            var exam = new AcademicTask
            {
                TaskID = Guid.NewGuid().ToString(),
                UserID = userId.ToString(),
                CourseID = courseId,
                TypeID = typeId,

                Name = name,
                DueDate = date,
                TaskStatus = status
            };
            
            _db.Tasks.Add(exam);
            await _db.SaveChangesAsync();
        }

        public async Task AllHomeworksAsync(string userId, string courseId, string name, DateTime dueDate)
        {
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
            await _db.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task<bool> CreateTask(AcademicTask task)
        {
            if (task == null) return false;

            _db.AcademicTasks.Add(task);
            await _db.SaveChangesAsync();   
            return true;
        }

        public async Task<bool> SaveNewTaskAsync(BaseTask task)
        {
            if (task == null) return false;

            // Clear tracker to prevent Entity Framework confusion
            _db.ChangeTracker.Clear();
            
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();   
            return true;
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

        public async Task<List<AcademicTaskType>> GetTaskTypesAsync()
        {
            return await _db.AcademicTaskTypes
                .AsNoTracking()
                .ToListAsync();
        }
         
        public async System.Threading.Tasks.Task<List<AcademicTask>>GetAllActivities(string typeId)
        {
            return await _db.AcademicTasks
                .Where(t => t.TypeID == typeId)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task<List<AcademicTask>>GetAllExam(string typeId)
        {
            return _db.AcademicTasks
                .Include(t => t.Type)
                .Where(t => t.TypeID == typeId)
                .ToList();
        }

        public async System.Threading.Tasks.Task<List<AcademicTask>>GetAllHomeworks(string typeId)
        {
            return await _db.AcademicTasks
                .Where(t => t.TypeID == typeId)
                .Include(t => t.Type)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<bool> UpdateTaskAsync(BaseTask updatedTask)
        {
            if (updatedTask == null) return false;
            
            // Clear tracker to prevent Entity Framework confusion
            _db.ChangeTracker.Clear();
            
            _db.Tasks.Update(updatedTask);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            var task = await _db.Tasks.FindAsync(taskId);
            if (task == null) return false;

            _db.Tasks.Remove(task);
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

            if (task.RiskLevel == Enums.RiskLevel.Critical) score += 5;
            else if (task.RiskLevel == Enums.RiskLevel.Warning) score += 3;
                else if (task.RiskLevel == Enums.RiskLevel.Stable) score += 1;

            return Math.Clamp(score, 1, 5);
        }
    }
}