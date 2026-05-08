namespace Acadeno.Backend.Models
{
    public class AcademicTask : BaseTask
    {
        public double TargetScore {get; set;}
        public double Score {get; set;}
        public double MaxScore {get; set;}

        //  Foreign Key
        public Guid TypeID {get; set;}
        public Guid CourseID {get; set;}
        

        //  Link to other tables
        public virtual AcademicTaskType? Type {get; set;}
    }
}