namespace Acadeno.Backend.DTOs
{
    public class CourseGradeResult
    {
        public double DefinedWeight { get; set; }
        public double UnallocatedWeight { get; set; }
        public double RunningPercentage { get; set; }
        public double FloorPercentage { get; set; }
        
        public string DisplayRunningGrade { get; set; } = string.Empty; 
        public string DisplayFloorGrade { get; set; } = string.Empty;
    }
}