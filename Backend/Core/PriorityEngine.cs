using BaseTask = Acadeno.Backend.Models.BaseTask;
using AcademicTask = Acadeno.Backend.Models.AcademicTask;

namespace Acadeno.Backend.Core
{
    public class PriorityEngine
    {
        public List<BaseTask> GetTopPriorities(int limit, List<BaseTask> tasks)
        {
            return RankTasks(tasks)
                .Take(limit)
                .ToList();
        }

        public double CalculatePriorityScore(BaseTask task)
        {
            //  These properties are on the BASE, so they are always visible.
            var date = task.DueDate; 
            double weight = 1.0; 

            //  Academic task check:
            if (task is AcademicTask academic)
            {
                weight = academic.Type?.Weight ?? 1.0;
            }

            return (100 / (date - DateTime.Now).TotalDays) + (weight * 10);
        }

        public List<BaseTask> RankTasks(List<BaseTask> tasks)
        {
            return tasks.OrderByDescending(t => CalculatePriorityScore(t)).ToList();
        }
    }
}