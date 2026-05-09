using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class AcademicYear
    {
        [Key]
        public string YearID {get; set;} = string.Empty;
        
        public string YearSpan {get; set;} = string.Empty;
        public bool IsCurrent {get; set;}
        public double? AYTargetGenAve {get; set;}
        public double? AYCalculatedGenAve {get; set;}

        // Foreign Key
        public string UserID {get; set;}  = string.Empty;

        // Holds many Terms
        public List<Term> Terms {get; set;} = new List<Term>();
    }
}
