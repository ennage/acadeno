using BaseTask = Acadeno.Backend.Models.BaseTask;
using AcademicTask = Acadeno.Backend.Models.AcademicTask;

namespace Acadeno.Backend.Core
{
    public class PriorityEngine
    {
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
    }
}