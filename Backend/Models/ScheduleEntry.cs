namespace Acadeno.Backend.Models
{
    public class ScheduleEntry
    {
        public string EntryID { get; set; } = string.Empty;
        public string CourseID { get; set; } = string.Empty;

        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Session { get; set; } = string.Empty; 
        public string Room { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;

        public double GetDurationInHours() => (EndTime - StartTime).TotalHours;
    }
}