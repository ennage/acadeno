namespace Acadeno.Backend.Models
{
    public class Grade
    {
        public string GradedID { get; set; } = string.Empty;
        public double RawGrade { get; set; }

        public double GetRawGrade() => RawGrade;
        public void SetRawGrade(double rawGrade) => RawGrade = rawGrade;
    
    }
}