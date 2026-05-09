using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Core
{
    public class GradingEngine
    {
        public double CalculateTaskPercentage(AcademicTask task)
        {
            if (task.MaxScore == 0) return 0;
            return (double)task.Score / task.MaxScore * 100;
        }

        public double CalculateCategoryGrade(AcademicTaskType type)
        {
            var gradedTasks = type.AcademicTasks
                .Where(t => t.Score >= 0)
                .ToList();      

            if(!gradedTasks.Any()) return 0;

            double TotalScore = gradedTasks.Sum(t => (double)t.Score);
            double TotalMaxScore = gradedTasks.Sum(t => (double)t.MaxScore);

            if (TotalMaxScore == 0) return 0;

            double performancePercentage = TotalScore / TotalMaxScore;
            return performancePercentage * type.Weight;
        }

        public double CalculateCourseGrade(Grade grade)
        {
           double totalWeightedGrade = 0;
           double totalWeight = 0;

            foreach (var type in grade.AcademicTaskTypes)
            {
                if (type.AcademicTasks == null || !type.AcademicTasks.Any()) continue;
               
                totalWeightedGrade += CalculateCategoryGrade(type);
                totalWeight += type.Weight;
            }

            if (totalWeight == 0) return 0;
            return totalWeightedGrade / totalWeight * 100;
        }

        public double CalculateTermGrade(Term term)
        {
           var coursesWithGrades = term.Courses
                .Where(c => c.ActualGrade != null && (c.Units ?? 0) > 0)
                .ToList(); 

            if (!coursesWithGrades.Any()) return 0;

            double totalWeightedGrade = coursesWithGrades
            .Sum(c => ConvertToGWA(c.ActualGrade.CourseGrade ?? 0, GradeScaleType.Default) * (c.Units ?? 0));

            double totalUnits = coursesWithGrades.Sum(c => c.Units ?? 0);
            double termGWA = totalUnits == 0 ? 0 : totalWeightedGrade / totalUnits;
            
            return Math.Round(termGWA, 2);
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