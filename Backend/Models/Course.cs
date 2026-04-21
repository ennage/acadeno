namespace Backend.Models
{
    namespace Acadeno.Backend.Models
    {
        public class Course
        {
            public string CourseId { get; set; } = string.Empty;
            public string CourseCode { get; set; } = string.Empty;
            public string CourseName { get; set; } = string.Empty;
            
            public double CurrentGrade { get; set; }
            public double TargetGrade { get; set; }
            public int Units {get; set; }

            public string GetCourseCode() => CourseCode;
            public void SetTargetGrade(double targetGrade) => TargetGrade = targetGrade;
        }
    }
}