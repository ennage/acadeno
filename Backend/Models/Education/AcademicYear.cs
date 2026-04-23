namespace Acadeno.Backend.Models.Education
{
    public class AcademicYear
    {
        public string YearID {get; set;} = string.Empty;
        public string YearSpan {get; set;} = string.Empty;
        public bool IsCurrent {get; set;}
        public double? AYTargetGenAve {get; set;}
        public double? AYCalculatedGenAve {get; set;}

        public string UserID {get; set;} = string.Empty;
        public List<Term> Terms {get; set;} = new List<Term>();
    }
}
