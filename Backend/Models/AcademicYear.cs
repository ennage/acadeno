using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class AcademicYear
    {
        [Key]
        public Guid YearID {get; set;} = Guid.NewGuid();
        
        public string YearSpan {get; set;} = string.Empty;
        public bool IsCurrent {get; set;}
        public double? AYTargetGenAve {get; set;}
        public double? AYCalculatedGenAve {get; set;}

        // Foreign Key
        public Guid UserID {get; set;}

        // Holds many Terms
        public List<Term> Terms {get; set;} = new List<Term>();
    }
}
