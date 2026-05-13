using System;
using System.Collections.Generic;
using System.Linq;
using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Simulation
{
    public class PriorityEngine
    {
        private readonly AppDbContext _db;

        public PriorityEngine(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Updates the RiskLevel of all tasks in the list and returns the top priorities.
        /// </summary>
        public List<BaseTask> GetTopPriorities(int count, List<BaseTask> allTasks)
        {
            if (allTasks == null || !allTasks.Any()) return new List<BaseTask>();

            foreach (var task in allTasks)
            {
                // Assign the "Actual" calculated risk level before sorting
                task.RiskLevel = DetermineActualRisk(task);
            }

            return allTasks
                .Where(t => t.TaskStatus != Status.Completed) // Don't prioritize finished work
                .OrderByDescending(t => t.RiskLevel)          // Critical first
                .ThenByDescending(t => CalculatePriorityScore(t)) 
                .ThenBy(t => t.DueDate)
                .Take(count)
                .ToList();
        }

        public RiskLevel DetermineActualRisk(BaseTask task)
        {
            if (task.TaskStatus == Status.Completed) return RiskLevel.Stable;

            var timeRemaining = task.DueDate - DateTime.Now;
            double days = timeRemaining.TotalDays;

            // 1. Determine "Stakes" (How much does this hurt if I fail?)
            double categoryWeight = 0;
            if (task is AcademicTask academic && academic.Type != null)
            {
                categoryWeight = academic.Type.Weight;
            }

            // 2. The Logic Matrix
            
            // CRITICAL: Overdue, Due Tomorrow, or a Heavy Exam due very soon
            if (days <= 1.0 || (days <= 3.0 && categoryWeight >= 30))
            {
                return RiskLevel.Critical;
            }

            // WARNING: Due within the week, or a medium-weight task (Quizzes/Activities)
            if (days <= 7.0 || categoryWeight >= 15)
            {
                return RiskLevel.Warning;
            }

            // STABLE: Far away or low weight
            return RiskLevel.Stable;
        }

        public double CalculatePriorityScore(BaseTask task)
        {
            var timeSpan = task.DueDate - DateTime.Now;
            double daysRemaining = timeSpan.TotalDays;

            // Use the newly determined RiskLevel for the score
            double taskWeight = ((int)task.RiskLevel + 1) * 10; 

            if (daysRemaining <= 0) return 200 + taskWeight; // Massive boost for overdue

            // Score increases as daysRemaining approaches 0
            return taskWeight / (daysRemaining > 0 ? daysRemaining : 0.1);
        }
    }
}