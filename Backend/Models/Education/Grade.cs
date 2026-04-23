namespace Acadeno.Backend.Models.Education
{
    public class Grade
    {
        public string GradeID {get; set;} = string.Empty;
        public double RawGrade {get; set;}

        public string CourseID {get; set;} = string.Empty;

        public List<AcademicTaskType> AcademicTaskTypes {get; set;} = new List<AcademicTaskType>();
    }   
}