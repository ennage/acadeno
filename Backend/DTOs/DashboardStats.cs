namespace Acadeno.Backend.DTOs
{
    public class DashboardStats
    {
        public string CurrentGWA { get; set; } = "0.00";
        public string TermGPA { get; set; } = "0.00";
        public double UnitsLoaded { get; set; } = 0;
        public string AcademicStanding { get; set; } = "No Data";
        public string University { get; set; } = "UNIVERSITY NOT SET";
    }
}