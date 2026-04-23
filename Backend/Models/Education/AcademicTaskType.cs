namespace Acadeno.Backend.Models.Education
{
    public class AcademicTaskType
    {
        public string TypeID {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public double Weight {get; set;}
        public double TargetScore {get; set;}

        public string GradeID { get; set; } = string.Empty;
        public List<AcademicTask> AcademicTasks {get; set;} = new List<AcademicTask>();
    }
}
