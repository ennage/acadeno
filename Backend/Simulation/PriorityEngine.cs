using System;
using System.Collections.Generic;
using System.Linq;
using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;
using Acadeno.Backend.Tools; // Required for AppDbContext

namespace Acadeno.Backend.Simulation
{
    public class PriorityEngine
    {
        private readonly AppDbContext _db;

        public PriorityEngine(AppDbContext db)
        {
            _db = db;
        }

        public List<BaseTask> GetTopPriorities(int count, List<BaseTask> allTasks)
        {
            if (allTasks == null || !allTasks.Any()) return new List<BaseTask>();

            return allTasks
                .OrderByDescending(t => CalculatePriorityScore(t)) // Highest score first
                .ThenBy(t => t.DueDate)                     // Then by earliest deadline
                .Take(count)
                .ToList();
        }

        public double CalculatePriorityScore(BaseTask task)
        {
            var timeSpan = task.DueDate - DateTime.Now;
            double daysRemaining = timeSpan.TotalDays;

            // 1. Base Weight from Difficulty (1-10 scale) and RiskLevel (Enum value)
            // High Risk + High Difficulty = High base weight
            double taskWeight = task.Difficulty + ((int)task.RiskLevel * 2);

            // 2. Factor in Urgency
            // If the task is overdue, we give it a massive boost
            if (daysRemaining <= 0)
            {
                return 100 + taskWeight; 
            }

            // 3. The Formula: Weight divided by Days Remaining
            // This makes the score grow exponentially as the deadline approaches
            // We use 0.5 to prevent division by zero and give 'Due Today' a high score
            return taskWeight / (daysRemaining > 0 ? daysRemaining : 0.5);
        }

        public List<BaseTask> RankTasks(List<BaseTask> tasks)
        {
            return tasks.OrderByDescending(t => CalculatePriorityScore(t)).ToList();
        }

        public int CalculateRiskLevel(string taskid)
        {
            var task = _db.Tasks.Find(taskid);
            if (task == null) return 1;

            int score = 1;
            var timeRemaining = task.DueDate - DateTime.Now;

            if (timeRemaining.TotalDays <= 1) score += 3;
            else if (timeRemaining.TotalDays <= 3) score += 2;
            else if (timeRemaining.TotalDays <= 7) score += 1;

            if (task.RiskLevel == RiskLevel.Critical) score += 5;
            else if (task.RiskLevel == RiskLevel.Warning) score += 3;
            else if (task.RiskLevel == RiskLevel.Stable) score += 1;

            return Math.Clamp(score, 1, 5);
        }
    }
}