using Acadeno.Backend.Tools;
using Acadeno.Backend.Simulation;
using Acadeno.Backend.Enums;
using Microsoft.EntityFrameworkCore;


namespace Acadeno.Backend.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _db;
        private readonly GradingEngine _engine;
       
        public DashboardService(AppDbContext db)
        {
            _db = db;
            _engine = new GradingEngine();
        }
         
        public class SimulatedTaskInput
        {
            public double score {get; set;}
            public double MaxScore{get; set;}
            public double Weight{get; set;}
        }
        public string GetAcademicStanding(double gwa)
        {
            if (gwa >= 1.00 && gwa<= 1.25) return "President Lister";
            if (gwa > 1.25 && gwa <= 1.75) return "Dean's Lister";
            if (gwa <= 2.25) return "Satisfactory";
            if (gwa <= 3.00) return "Passing";
            return "Failing";
        }
        

        public async Task<object> GetStats(string userId)
        {
            var courses = await _db.Courses
                .Where(c => c.UserID == userId)
                .ToListAsync();

            double totalUnit = courses.Sum(c => c.Units.GetValueOrDefault(0));

            double weightedGwaSum = courses.Sum(c => (c.ActualGrade?.CourseGrade ?? 0) * c.Units.GetValueOrDefault(0));

            double currentGwa = (totalUnit > 0) ? weightedGwaSum / totalUnit : 0;

            return new
            {
                CurrentGWA = Math.Round(currentGwa, 2),
                UnitsLoaded = totalUnit,
                AcademicStanding = GetAcademicStanding(currentGwa)
            };
        }

        public async Task<object> SimulateAcademicStatus(List<SimulatedTaskInput> inputs)
        {
            double totalWeightedScore = 0;
           double totalWeight = 0;
           double highestGrade = 0;

            foreach (var input in inputs)
            {
                double CategoryPercentage = (input.MaxScore > 0) ? input.score / input.MaxScore * 100 : 0;

                if (CategoryPercentage > highestGrade)
                {
                    highestGrade = CategoryPercentage;
                }

                totalWeightedScore += CategoryPercentage *(input.Weight / 100);
                totalWeight += input.Weight;
            }

            double finalPercentage = (totalWeight > 0) ? (totalWeightedScore / (totalWeight / 100)) : 0;

            double averageGrade = finalPercentage;

            double simulatedGWA = _engine.ConvertToGWA(finalPercentage  , GradeScaleType.Default);
            string academicStanding = GetAcademicStanding(simulatedGWA);
            
            return new
            {
                simulatedGWA = Math.Round(simulatedGWA, 2),
                academicStanding = academicStanding,
                AverageGrade = $"{Math.Round(averageGrade, 2)}%",
                HighestGrade = $"{Math.Round(highestGrade, 2)}%",
                TotalCourse = inputs.Count
            };
        }
    }
}
