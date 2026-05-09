using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class Grade
    {
        [Key]
        public string GradeID {get; set;} = string.Empty;
        
        public double? TargetCourseGrade {get; set;}
        public double? CourseGrade {get; set;}

        // Foreign Key
        public string CourseID {get; set;}  = string.Empty;

        public List<AcademicTaskType> AcademicTaskTypes {get; set;} = new List<AcademicTaskType>();
    }   
}