using Acadeno.Backend.Models;

namespace Acadeno.Backend.Models.Education
{
    public class AcademicTask
    {
        public string CourseID { get; set; } = string.Empty;
        public double Score { get; set; }
        public double MaxScore { get; set; }
        public string UserID { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
    }
}