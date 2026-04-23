using Acadeno.Backend.Models.Enums;

namespace Acadeno.Backend.Models.Education
{
    public class AcademicTask
    {
        public double Score {get; set;}
        public double MaxScore {get; set;}
        public DateTime DueDate {get; set;}
        public RiskLevel RiskLevel {get; set;}

        //  Foreign Key
        public string UserID {get; set;} = string.Empty;
        public string CourseID {get; set;} = string.Empty;
        public string TypeID {get; set;} = string.Empty;

        //  Link to other tables
        public virtual AcademicTaskType? Type {get; set;}
    }
}