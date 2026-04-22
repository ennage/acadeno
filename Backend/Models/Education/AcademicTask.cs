using Acadeno.Backend.Models.Schedule;
namespace Acadeno.Backend.Models.Education
{
    public class AcademicTask : BaseTask
    {
        public double Score {get; set;}
        public double MaxScore {get; set;}

        //  Foreign Key
        public string? CourseID {get; set;} = string.Empty;
    }
}
