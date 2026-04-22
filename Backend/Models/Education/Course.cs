namespace Acadeno.Backend.Models.Education
{
    public class Course
    {
        public string CourseID {get; set;} = string.Empty;
        public string CourseCode {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public int? Units {get; set;}

        //  Foreign Key
        public string TermID {get; set;} = string.Empty;
        
        //  Holds one Grade
        public Grade ActualGrade {get; set;} = new Grade();

        
    }
}
