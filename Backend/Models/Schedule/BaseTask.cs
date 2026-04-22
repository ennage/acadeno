using Acadeno.Backend.Models.Enums;
namespace Acadeno.Backend.Models.Schedule
{
    public class BaseTask
    {
        public string TaskID {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public int Difficulty {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime DueDate {get; set;}
        public Status TaskStatus {get; set;} = Status.Pending;
        public RiskLevel RiskLevel {get; set;}

        //  Foreign Key
        public string UserID {get; set;} = string.Empty;
    }
}
