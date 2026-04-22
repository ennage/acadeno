namespace Acadeno.Backend.Models.Education
{
    public class Grade
    {
        public string GradeID {get; set;} = string.Empty;
        public double? TargetCourseGrade {get; set;}
        public double? CourseGrade {get; set;}

        //  Foreign Key
        public string CourseID {get; set;} = string.Empty;

        //  Holds many Academic Task Types
        public List<AcademicTaskType> AcademicTaskTypes {get; set;} = new List<AcademicTaskType>();
    }   
}