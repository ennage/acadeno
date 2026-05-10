using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadeno.Backend.Models
{
    public class Course
    {
        [Key]
        public string CourseID {get; set;} = string.Empty;
        
        public string CourseCode {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public int? Units {get; set;}
        public Grade ActualGrade {get; set;} = new Grade();
        
        // Foreign Keys
        [ForeignKey("Term")]
        public string TermID {get; set;} = string.Empty;
        public string UserID {get; set;} = string.Empty;

        // Navigation Property
        public virtual Term Term {get; set;} = null!;

        // Holds Schedule Entries
        public List<ScheduleEntry> ScheduleEntries {get; set;} = new List<ScheduleEntry>();
    }
}
