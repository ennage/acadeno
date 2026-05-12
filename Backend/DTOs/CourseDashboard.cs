namespace Acadeno.Backend.DTOs
{
    public class CourseDashboard
    {
        public string CourseID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public double CurrentGrade { get; set; }
        public string TermID { get; set; } = string.Empty;
    }
}