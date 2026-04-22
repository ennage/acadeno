namespace Acadeno.Backend.Models.Education
{
    public class Term
    {
        public string TermID {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public bool IsCurrent {get; set;}
        public double? TermTargetGenAve {get; set;}
        public double? TermCalculatedGenAve {get; set;}
        //  Foreign Key
        public string YearID {get; set;} = string.Empty;

        //  Holds many Courses:
        public List<Course> Courses {get; set;} = new List<Course>();
    }
}
