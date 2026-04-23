using Acadeno.Backend.Models.Education;
using Acadeno.Backend.Models.Enums;

namespace Acadeno.Backend.Models.Admin
{
    public class User
    {
        //  Primary Key for DB
        public string UserID {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public string University {get; set;} = string.Empty;
        public string Program {get; set;} = string.Empty;
        
        //  Login purposes
        public string Email {get; set;} = string.Empty;
        public string Password {get; set;} = string.Empty;

        public GradeScaleType PreferredScale {get; set;}

        //  Holds many Academic Years:
        public List<AcademicYear> Years {get; set;} = new List<AcademicYear>();
    }
}
