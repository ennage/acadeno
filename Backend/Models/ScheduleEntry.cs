using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class ScheduleEntry
    {
        [Key]
        public string UserID {get; set;} = string.Empty;

        public string EntryID {get; set;} = string.Empty;
        public string CourseID {get; set;} = string.Empty;

        public TimeSpan StartTime {get; set;}
        public TimeSpan EndTime {get; set;}
        public string DayOfWeek {get; set;} = string.Empty;

        public string Session {get; set;} = string.Empty; 
        public string Room {get; set;} = string.Empty;
        public string Building {get; set;} = string.Empty;

        public double GetDurationInHours() => (EndTime - StartTime).TotalHours;
        public string FormattedTime() => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";

        private string FormatedTime(TimeSpan time)
        {
            return DateTime.Today.Add(time).ToString("hh:mm tt");
        }
    }
}