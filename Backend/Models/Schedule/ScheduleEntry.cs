namespace Acadeno.Backend.Models.Schedule
{
    public class ScheduleEntry
    {
        public string EntryID {get; set;} = string.Empty;
        public string Label {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public string Session {get; set;} = string.Empty;
        public string Room {get; set;} = string.Empty;
        public string Building {get; set;} = string.Empty;
        public DateTime StartTime {get; set;}
        public DateTime EndTime {get; set;}
        public DayOfWeek Day {get; set;}
    }
}