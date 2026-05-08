using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models
{
    public class AcademicTaskType
    {
        [Key]
        public Guid TypeID {get; set;} = Guid.NewGuid();

        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public double Weight {get; set;}

        //  Foreign Key
        public Guid GradeID {get; set;}

        //  Holds many Academic Tasks
        public List<AcademicTask> AcademicTasks {get; set;} = new List<AcademicTask>();
    }
}
