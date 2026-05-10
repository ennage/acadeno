using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadeno.Backend.Models
{
    public class AcademicTaskType
    {
        [Key]
        public string TypeID {get; set;} = string.Empty;

        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public double Weight {get; set;}

        //  Foreign Key
        public string GradeID {get; set;} = string.Empty;
        public string DefID {get; set;} = string.Empty;

        // Navigation Property
        [ForeignKey("DefID")]
        public virtual TaskTypeDefinition? Definition { get; set; }

        //  Holds many Academic Tasks
        public List<AcademicTask> AcademicTasks {get; set;} = new List<AcademicTask>();
    }
}
