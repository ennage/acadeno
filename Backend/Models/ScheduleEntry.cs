using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadeno.Backend.Models
{
    public class ScheduleEntry
    {
        [Key]
        public Guid EntryID {get; set;} = Guid.NewGuid();

        // Foreign Key
        public Guid UserID {get; set;}
        [ForeignKey("UserID")]
        public Guid CourseID {get; set;}
        [ForeignKey("CourseID")]

        public Course? Course {get; set;}
        public User? User {get; set;}

        public string Label {get; set;}  = string.Empty;
        public string Description {get; set;} = string.Empty;
        public int Day {get; set;}
        public bool IsRepeating {get; set;} // True for weekly classes, False for one-off events

        public DateTime StartTime {get; set;}
        public DateTime EndTime {get; set;}
        
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