namespace Acadeno.Backend.Models;

public class CourseTimeTable
{
    public List<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();

    public List<ScheduleEntry> GenerateWeeklyView()
    {
        return ScheduleEntries
            .OrderBy(e => (int)Enum.Parse<DayOfWeek>(e.DayOfWeek))
            .ThenBy(e => e.StartTime)
            .ToList();
    }

    public bool isConflict(ScheduleEntry newEntry)
    {
        foreach (var entry in ScheduleEntries)
        {
            if (newEntry.DayOfWeek == entry.DayOfWeek &&
                newEntry.StartTime < entry.EndTime &&
                newEntry.EndTime > entry.StartTime)
            {
                return true; 
            }
        }
        return false; 
    }
}