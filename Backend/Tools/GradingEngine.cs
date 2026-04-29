using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Tools
{
    public class GradingEngine
    {
        public double CalculateTaskPercentage(AcademicTask task)
        {
            double score = task.Score;
            double maxScore = task.MaxScore;
            
            if (maxScore == 0) return 0;

            return (score / maxScore) * 100;

        }

        public double CalculatCategoryScore(List<AcademicTask> tasks)
        {
            if (tasks.Count == 0) return 0;
            return tasks.Average(t => CalculateTaskPercentage(t));
        }

        public double ConvertToGPA(double rawGrade, GradeScaleType scale)
        {
            if (rawGrade >= 97) return 5.00;
            if (rawGrade >= 94) return 3.00;
            if (rawGrade >= 91) return 2.75;
            if (rawGrade >= 88) return 2.50;
            if (rawGrade >= 85) return 1.75;
            if (rawGrade >= 82) return 1.50;
            if (rawGrade >= 79) return 1.25;
            return 0.00;

        }
        public double ConvertToGWA(double rawGrade, GradeScaleType scale)
        {
            if (rawGrade >= 98) return 1.00;
            if (rawGrade >= 94) return 1.25;
            if (rawGrade >= 91) return 1.5;
            if (rawGrade >= 88) return 1.75;
            if (rawGrade >= 85) return 2.0;
            if (rawGrade >= 82) return 2.25;
            if (rawGrade >= 79) return 2.5;
            if (rawGrade >= 76) return 2.75;
            if (rawGrade >= 75) return 3.0;
            return 5.0;
        }

        public string ConvertToLetter(double rawGrade, GradeScaleType scale) 
        {
            if (rawGrade >= 97) return "A+";
            if (rawGrade >= 94) return "A";
            if (rawGrade >= 91) return "A-";
            if (rawGrade >= 88) return "B+";
            if (rawGrade >= 85) return "B";
            if (rawGrade >= 82) return "B-";
            if (rawGrade >= 79) return "C+";
            if (rawGrade >= 76) return "C";
            if (rawGrade >= 75) return "C-";
            return "F";
        }
    }
}