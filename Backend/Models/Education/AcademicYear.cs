namespace Acadeno.Backend.Models.Education
{
    public class AcademicYear
    {
        public string YearID {get; set;} = string.Empty;
        public string YearSpan {get; set;} = string.Empty;
        public bool IsCurrent {get; set;}

        //  Foreign Key
        public string UserID {get; set;} = string.Empty;

        //  Holds many Terms:
        public List<Term> Terms {get; set;} = new List<Term>();
    }
}
