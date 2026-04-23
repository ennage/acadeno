using Task = Acadeno.Backend.Models.Education.AcademicTask;

namespace Acadeno.Backend.Tools
{
    public class PriorityEngine
    {
        public List<Task> GetTopProrities(int limit, List<Task> tasks)
        {
            return RankTasks(tasks)
                .Take(limit)
                .ToList();
        }

        public double CalculatePriorityScore(Task task)
        {
            var daysUntilDue = (task.DueDate - DateTime.Now).TotalDays;
            double timeFactor = daysUntilDue > 0 ? 100 : (10 / daysUntilDue);

            // FIX: Reach into the 'Type' property to get the Weight
            // We use ?. to be safe just in case the Type is null
            double weight = task.Type?.Weight ?? 1.0; 
            return timeFactor + (weight * 10);
        }

        public List<Task> RankTasks(List<Task> tasks)
        {
            return tasks.OrderByDescending(t => CalculatePriorityScore(t)).ToList();
        }
    }
}