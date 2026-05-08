using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Core
{
    public class GradingEngine
    {
        public double CalculateTaskPercentage(AcademicTask task)
        {
            if (task.MaxScore == 0) return 0;
            return task.Score / task.MaxScore * 100;
        }

        public double CalculatCategoryScore(AcademicTaskType type)
        {
            var gradedTasks = type.AcademicTasks
                .Where(t => t.Score >= 0)
                .ToList();      

            double TotalScore = gradedTasks.Sum(t => t.Score);
            double TotalMaxScore = gradedTasks.Sum(t => t.MaxScore);

            if (TotalMaxScore == 0) return 0;
            return TotalScore / TotalMaxScore * 100;
        }

        public double CalculateCourseGrade(Grade grade)
        {
           double totalWeightedGrade = 0;
           double totalWeight = 0;

            foreach (var type in grade.AcademicTaskTypes)
            {
                if (type.AcademicTasks == null || !type.AcademicTasks.Any()) continue;
               
                double categoryAve = CalculatCategoryScore(type);
                totalWeightedGrade += categoryAve * (type.Weight / 100);
                totalWeight += type.Weight;
            }

            if (totalWeight == 0) return 0;
            return totalWeightedGrade / (totalWeight / 100);
        }

        public double CalculateTermGrade(Term term)
        {
           var coursesWithGrades = term.Courses
                .Where(c => c.ActualGrade != null && (c.Units ?? 0) > 0)
                .ToList();

            if (!coursesWithGrades.Any()) return 0;

            double totalWeightedGrade = coursesWithGrades
            .Sum(c => ConvertToGWA(c.ActualGrade.CourseGrade ?? 0, GradeScaleType.Default) * (c.Units ?? 0));

            int totalUnits = coursesWithGrades.Sum(c => c.Units ?? 0);

            return totalUnits == 0 ? 0 : totalWeightedGrade / totalUnits;
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