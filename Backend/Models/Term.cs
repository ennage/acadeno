using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadeno.Backend.Models
{
    public class Term
    {
        [Key]
        public string TermID {get; set;} = string.Empty;
        
        public string Name {get; set;} = string.Empty;
        public bool IsCurrent {get; set;}

        public double? TermTargetGenAve {get; set;}
        public double? TermCalculatedGenAve {get; set;} 

        //  Foreign Key
        [ForeignKey("AcademicYear")]
        public string YearID {get; set;}  = string.Empty;

        // Navigation Property
        public virtual AcademicYear AcademicYear {get; set;} = null!;
        
        //  Holds many Courses
        public List<Course> Courses {get; set;} = new List<Course>();
    }
}
