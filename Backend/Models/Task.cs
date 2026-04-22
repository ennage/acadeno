using Acadeno.Backend.Models.Enums;

namespace Acadeno.Backend.Models   
{
    public class Task
    {
        public string TaskID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public string UserID { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public Status taskStatus { get; set; }
        public RiskLevel riskLevel { get; set; }
        
        public string GetName() => Name;
        public void SetName(string name) => Name = name;

        public int GetDifficulty() => Difficulty;
        public void SetDifficulty(int difficulty) => Difficulty = difficulty;

        public DateTime GetStartDate() => StartDate;
        public void SetStartDate(DateTime startDate) => StartDate = startDate;

        public DateTime GetDueDate() => DueDate;
        public void SetDueDate(DateTime dueDate) => DueDate = dueDate;

        public Status GetTaskStatus() => taskStatus;
        public void SetTaskStatus(Status status) => taskStatus = status;

        public RiskLevel GetRiskLevel() => riskLevel;
        public void SetRiskLevel(RiskLevel level) => riskLevel = level;
    }
}