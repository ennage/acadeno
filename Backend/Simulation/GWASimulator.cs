using Acadeno.Backend.Tools;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Simulation
{
    public class GWASimulator
    {
        private readonly AppDbContext _db;
        private readonly GradingEngine _engine;

        public GWASimulator(AppDbContext db)
        {
            _db = db;
            _engine = new GradingEngine();        
        }
        
        public class SimulatedTaskInput
        {
            public double score { get; set; }
            public double MaxScore { get; set; }
            public double Weight { get; set; }
        }
        public object SimulateAcademicStatus(string userId, string termId, List<SimulatedTaskInput> inputs)
        {
            double totalWeightedScore = 0;
            double totalWeight = 0;

            foreach (var input in inputs)
            {
                double CategoryPercentage = (input.MaxScore > 0) ? (input.score / input.MaxScore) * 100 : 0;

                totalWeightedScore += CategoryPercentage *(input.Weight / 100);
                totalWeight += input.Weight;
            }

            double finalPercentage = (totalWeight > 0) ? (totalWeightedScore / (totalWeight / 100)) : 0;

            double simulatedGWA = _engine.ConvertToGWA(finalPercentage  , GradeScaleType.Default);
            string academicStanding = GetAcademicStanding(simulatedGWA);
            
            return new
            {
                simulatedGWA = Math.Round(simulatedGWA, 2),
                academicStanding = academicStanding,
                RawPercentage = Math.Round(finalPercentage, 2)
            };
        }
        public string GetAcademicStanding(double gwa)
        {
            if (gwa <= 1.25) return "Excellent";
            if (gwa <= 1.75) return "Good";
            if (gwa <= 2.25) return "Satisfactory";
            if (gwa <= 3.00) return "Needs Improvement";

            return "Failing";
        }
    }
}