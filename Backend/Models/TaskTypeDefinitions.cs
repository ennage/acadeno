using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class TaskTypeDefinition
    {
        [Key]
        public string DefID {get; set;} = string.Empty;

        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;}

        /* Navigation Property: One definition (e.g., "Quizzes") can be used by 
        many different AcademicTaskTypes across different courses. */
        public virtual List<AcademicTaskType> AcademicTaskTypes {get; set;} = new List<AcademicTaskType>();
    }
}