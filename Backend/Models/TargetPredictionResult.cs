namespace Acadeno.Backend.Models
{
    public class TargetPredictionResult
    {
        public bool IsPossible { get; set; }
        public double RequiredAverage { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}