using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class Course
    {
        [Key]
        public Guid CourseID {get; set;} = Guid.NewGuid();
        
        public string CourseCode {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public int? Units {get; set;}
        public Grade ActualGrade {get; set;} = new Grade();
        public string Room {get; set;} = string.Empty;
        
        // Foreign Keys
        public Guid TermID {get; set;}
        public Guid UserID {get; set;}

        // Holds Schedule Entries
        public List<ScheduleEntry> ScheduleEntrys {get; set;} = new List<ScheduleEntry>();
    }
}
