namespace Acadeno.Backend.DTOs
{
    public class AcademicStandingResult
    {
        public double CumulativeGwa { get; set; }
        public double CurrentTermGwa { get; set; }
        public string Standing { get; set; } = "N/A";
        public string StandingColor { get; set; } = "#64748b";
        public double CirclePercentage { get; set; }
    }
}