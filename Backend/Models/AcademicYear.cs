namespace Acadeno.Backend.Models
{
    public class AcademicYear
    {
        public string YearId { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public bool IsCurrent { get; set; }

        public string GetYear() => Year;
    }
}