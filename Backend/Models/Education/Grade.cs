namespace Acadeno.Backend.Models.Education
{
    public class Grade
    {
        public string GradeID {get; set;} = string.Empty;
        public double RawGrade {get; set;}

        //  Foreign Key
        public string CourseID {get; set;} = string.Empty;

        //  Holds many Academic Task Types
        public List<AcademicTaskType> AcademicTaskTypes {get; set;} = new List<AcademicTaskType>();
    }   
}