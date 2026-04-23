using System.ComponentModel.DataAnnotations;
namespace Acadeno.Backend.Models.Education
{
    public class AcademicTaskType
    {
        [Key]
        public string TypeID {get; set;} = string.Empty;

        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public double Weight {get; set;}

        //  Foreign Key
        public string? GradeID { get; set; } = string.Empty;

        //  Holds many Academic Tasks
        public List<AcademicTask> AcademicTasks {get; set;} = new List<AcademicTask>();
    }
}
