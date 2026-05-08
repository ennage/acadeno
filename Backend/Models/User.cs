using System.ComponentModel.DataAnnotations;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Models
{
    public class User
    {
        [Key]
        public Guid UserID {get; set;} = Guid.NewGuid();
        
        public string Name {get; set;} = string.Empty;
        public string University {get; set;} = string.Empty;
        public string Program {get; set;} = string.Empty;
        
        //  Login purposes
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;
        public GradeScaleType PreferredScale {get; set;}

        //  Holds many Academic Years
        public List<AcademicYear> Years {get; set;} = new List<AcademicYear>();
    }
}
