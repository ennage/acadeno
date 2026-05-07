using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class Course
    {
        [Key]
        public string CourseID {get; set;} = string.Empty;
        public string UserID {get; set;} = string.Empty;
        public string Professor {get; set;} = string.Empty;
        
        public string CourseCode {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public int? Units {get; set;}

        public List<ScheduleEntry> ScheduleEntrys {get; set;} = new List<ScheduleEntry>();
        public string Room {get; set;} = string.Empty; 
        public string TermID {get; set;} = string.Empty;
        
        public Grade ActualGrade {get; set;} = new Grade();

        
    }
}
