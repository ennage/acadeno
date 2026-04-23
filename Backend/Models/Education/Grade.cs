namespace Acadeno.Backend.Models.Education
{
    public class Grade
    {
        public string GradeID {get; set;} = string.Empty;
        public double? TargetCourseGrade {get; set;}
        public double? CourseGrade {get; set;}

        public string CourseID {get; set;} = string.Empty;

        public List<AcademicTaskType> AcademicTaskTypes {get; set;} = new List<AcademicTaskType>();
    }   
}