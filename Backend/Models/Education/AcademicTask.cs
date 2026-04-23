using System.ComponentModel.DataAnnotations;
using Acadeno.Backend.Models.Enums;

namespace Acadeno.Backend.Models.Education
{
    public class AcademicTask : BaseTask
    {
        public double TargetScore {get; set;}
        public double Score {get; set;}
        public double MaxScore {get; set;}

        //  Foreign Key
        public string TypeID {get; set;} = string.Empty;
        public string CourseID {get; set;} = string.Empty;
        

        //  Link to other tables
        public virtual AcademicTaskType? Type {get; set;}
    }
}