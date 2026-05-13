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
        // Unified save method for all Academic Tasks
        public async Task<bool> SaveNewAcademicTaskAsync(AcademicTask newTask)
        {
            // Generate an ID if the UI didn't provide one
            if (string.IsNullOrEmpty(newTask.TaskID))
            {
                newTask.TaskID = Guid.NewGuid().ToString();
            }

            _db.ChangeTracker.Clear();
            
            // Save the ENTIRE object (Description, Scores, Difficulty included!)
            await _db.AcademicTasks.AddAsync(newTask);
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
            if (updatedTask == null || string.IsNullOrEmpty(updatedTask.TaskID)) return false;

            var existingTask = await _db.Tasks.FindAsync(updatedTask.TaskID);
            if (existingTask == null) return false;

            existingTask.Name = updatedTask.Name;
            existingTask.Description = updatedTask.Description;
            existingTask.StartDate = updatedTask.StartDate;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.Difficulty = updatedTask.Difficulty;
            existingTask.RiskLevel = updatedTask.RiskLevel;
            existingTask.TaskStatus = updatedTask.TaskStatus;

            // If it's an Academic Task, safely map the academic fields!
            if (existingTask is AcademicTask existingAcademic && updatedTask is AcademicTask updatedAcademic)
            {

                existingAcademic.CourseID = updatedAcademic.CourseID;
                existingAcademic.TypeID = updatedAcademic.TypeID;
                existingAcademic.TargetScore = updatedAcademic.TargetScore;
                existingAcademic.Score = updatedAcademic.Score;
                existingAcademic.MaxScore = updatedAcademic.MaxScore;
            }

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
        public BaseTask CloneTask(BaseTask original)
        {
            // Check if it's an Academic Task first
            if (original is AcademicTask academic)
            {
                return new AcademicTask
                {
                    TaskID = academic.TaskID,
                    UserID = academic.UserID,
                    Name = academic.Name,
                    Description = academic.Description,
                    StartDate = academic.StartDate,
                    DueDate = academic.DueDate,
                    Difficulty = academic.Difficulty,
                    RiskLevel = academic.RiskLevel,
                    TaskStatus = academic.TaskStatus,
                    
                    // THE CRITICAL MISSING PIECES:
                    CourseID = academic.CourseID,
                    TypeID = academic.TypeID,
                    TargetScore = academic.TargetScore,
                    Score = academic.Score,
                    MaxScore = academic.MaxScore
                };
            }
            
            // Fallback for standard tasks (Reminders, general to-dos)
            return new BaseTask
            {
                TaskID = original.TaskID,
                UserID = original.UserID,
                Name = original.Name,
                Description = original.Description,
                StartDate = original.StartDate,
                DueDate = original.DueDate,
                Difficulty = original.Difficulty,
                RiskLevel = original.RiskLevel,
                TaskStatus = original.TaskStatus
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