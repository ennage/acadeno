using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models.Education
{
    public class Course
    {
        [Key]
        public string CourseID {get; set;} = string.Empty;
        
        public string CourseCode {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public int? Units {get; set;}

        public string TermID {get; set;} = string.Empty;
        
        public Grade ActualGrade {get; set;} = new Grade();

        
    }
}
