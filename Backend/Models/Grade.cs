using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class Grade
    {
        [Key]
        public Guid GradeID {get; set;} = Guid.NewGuid();
        
        public double? TargetCourseGrade {get; set;}
        public double? CourseGrade {get; set;}

        public Guid CourseID {get; set;}

        public List<AcademicTaskType> AcademicTaskTypes {get; set;} = new List<AcademicTaskType>();
    }   
}