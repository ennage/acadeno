using Acadeno.Backend.DTOs;
using Acadeno.Backend.Enums;
using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class TaskService
    {
        private readonly AppDbContext _db;

        public TaskService(AppDbContext db)
        {
            _db = db;
        }

        #region CREATE & SAVE

        // Unified save method for all Academic Tasks (Exams, Quizzes, Activities, etc.)
        public async Task<bool> SaveNewAcademicTaskAsync(string userId, string courseId, string typeId, string name, DateTime dueDate, Status status = Status.Pending)
        {
            var task = new AcademicTask
            {
                // Generates the standard 36-char ID. 
                // Add "acade-" + if you prefer your custom prefix!
                TaskID = Guid.NewGuid().ToString(), 
                UserID = userId,
                CourseID = courseId,
                TypeID = typeId, 
                Name = name,
                DueDate = dueDate,
                TaskStatus = status,
                
                // Defaults for the Grading Engine
                Score = 0,
                MaxScore = 100 
            };

            await _db.AcademicTasks.AddAsync(task);
            return await _db.SaveChangesAsync() > 0;
        }

        /// Specifically for non-academic tasks or general base tasks
        public async Task<bool> SaveGenericTaskAsync(BaseTask task)
        {
            if (string.IsNullOrEmpty(task.TaskID)) 
                task.TaskID = Guid.NewGuid().ToString();

            await _db.Tasks.AddAsync(task);
            return await _db.SaveChangesAsync() > 0;
        }

        #endregion

        #region READ METHODS

        // Fetches all tasks for a user.
        // Includes Course and Type info so the UI can display "Math - Quiz 1"
        public async Task<List<AcademicTask>> GetUserTasksAsync(string userId)
        {
            return await _db.AcademicTasks
                .AsNoTracking()
                .Include(t => t.Course)  // Allows @task.Course.Name in UI
                .Include(t => t.Type)    // Allows @task.Type.Name (Quizzes, Exams) in UI
                .Where(t => t.UserID == userId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        // Use this for specific filtered views (e.g., "Just Exams")
        public async Task<List<AcademicTask>> GetTasksByTypeAsync(string userId, string typeId)
        {
            return await _db.AcademicTasks
                .AsNoTracking()
                .Include(t => t.Course)
                .Where(t => t.UserID == userId && t.TypeID == typeId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<List<AcademicTaskType>> GetAvailableTaskTypesAsync()
        {
            return await _db.AcademicTaskTypes
                .AsNoTracking()
                .ToListAsync();
        }

        #endregion

        #region UPDATE & DELETE

        public async Task<bool> UpdateTaskAsync(BaseTask updatedTask)
        {
            if (updatedTask == null) return false;
            
            // Critical for Blazor: Prevents EF from tracking two copies of the task
            _db.ChangeTracker.Clear();
            
            _db.Tasks.Update(updatedTask);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            var task = await _db.Tasks.FindAsync(taskId);
            if (task == null) return false;

            _db.Tasks.Remove(task);
            return await _db.SaveChangesAsync() > 0;
        }

        #endregion

        #region UTILITIES

        // For EditReminders Popup
        public BaseTask? CloneTask(BaseTask? original)
        {
            if (original == null) return null;

            if (original is AcademicTask ac)
            {
                return new AcademicTask {
                    TaskID = ac.TaskID,
                    UserID = ac.UserID,
                    Name = ac.Name,
                    Description = ac.Description,
                    StartDate = ac.StartDate,
                    DueDate = ac.DueDate,
                    TaskStatus = ac.TaskStatus,
                    CourseID = ac.CourseID,
                    TypeID = ac.TypeID,
                    RiskLevel = ac.RiskLevel,
                    Score = ac.Score,
                    MaxScore = ac.MaxScore
                };
            }
            
            return new BaseTask {
                TaskID = original.TaskID,
                UserID = original.UserID,
                Name = original.Name,
                Description = original.Description,
                StartDate = original.StartDate,
                DueDate = original.DueDate,
                TaskStatus = original.TaskStatus,
                RiskLevel = original.RiskLevel
            };
        }

        // Deep clones an academic task for simulation or temporary UI edits
        public AcademicTask CloneAcademicTask(AcademicTask original)
        {
            return new AcademicTask {
                TaskID = original.TaskID,
                UserID = original.UserID,
                CourseID = original.CourseID,
                TypeID = original.TypeID,
                Name = original.Name,
                Description = original.Description,
                DueDate = original.DueDate,
                TaskStatus = original.TaskStatus,
                Score = original.Score,
                MaxScore = original.MaxScore,
                RiskLevel = original.RiskLevel
            };
        }

        #endregion
    }
}