using Acadeno.Backend.Models;

using Task = Acadeno.Backend.Models.Task;

namespace Acadeno.Backend.Services;

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
        return timeFactor + (task.ImportanceWeight * 10);
    }

    public List<Task> RankTasks(List<Task> tasks)
    {
        return tasks.OrderByDescending(t => CalculatePriorityScore(t)).ToList();
    }
}