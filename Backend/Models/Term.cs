using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class Term
    {
        [Key]
        public Guid TermID {get; set;} = Guid.NewGuid();
        
        public string Name {get; set;} = string.Empty;
        public bool IsCurrent {get; set;}

        public double? TermTargetGenAve {get; set;}
        public double? TermCalculatedGenAve {get; set;} 

        //  Foreign Key
        public Guid YearID {get; set;}
        
        //  Holds many Courses
        public List<Course> Courses {get; set;} = new List<Course>();
    }
}
