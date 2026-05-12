using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;
using System.Linq;
using System;

namespace Acadeno.Backend.Simulation
{
    public class TargetSimulator
    {
        private readonly GradingEngine _engine;
        private readonly GradeConverter _converter;

        public TargetSimulator(GradingEngine engine, GradeConverter converter)
        {
            _engine = engine;
            _converter = converter;
        }

        // --- TARGET PREDICTION ---
        
        // 1. Predict what is needed in a single COURSE
        public TargetPredictionResult PredictCourseTarget(Grade grade, string targetInput, GradeScaleType scale)
        {
            // Convert their target (e.g. "1.50") into the minimum percentage needed (e.g. 91%)
            double targetPercentage = _converter.ConvertInputToPercentage(targetInput, scale);
            var currentStatus = _engine.CalculateCourseGrade(grade, scale);

            if (currentStatus.UnallocatedWeight <= 0)
            {
                return new TargetPredictionResult { IsPossible = false, RequiredAverage = 0, Message = "No remaining tasks." };
            }

            // How many percentage points are missing?
            double pointsNeeded = targetPercentage - currentStatus.FloorPercentage;

            if (pointsNeeded <= 0)
            {
                return new TargetPredictionResult { IsPossible = true, RequiredAverage = 0, Message = "Target already achieved!" };
            }

            // Calculate the required average on the remaining weight
            double requiredAvg = pointsNeeded / (currentStatus.UnallocatedWeight / 100);

            return new TargetPredictionResult
            {
                IsPossible = requiredAvg <= 100, // If they need > 100%, it's mathematically impossible
                RequiredAverage = Math.Round(requiredAvg, 2),
                Message = requiredAvg > 100 ? "Mathematically impossible." : $"You need to average {Math.Round(requiredAvg, 2)}% on remaining tasks."
            };
        }

        // 2. Predict what GPA is needed for the remaining TERMS
        public TargetPredictionResult PredictTermTarget(Term term, double targetTermGWA, GradeScaleType scale)
        {
            var courses = term.Courses?.Where(c => (c.Units ?? 0) > 0).ToList();
            if (courses == null || !courses.Any()) 
                return new TargetPredictionResult { IsPossible = false, Message = "No courses found." };

            double currentQualityPoints = 0;
            double completedUnits = 0;
            double remainingUnits = 0;

            foreach (var course in courses)
            {
                // If a course has a final grade, it is completed
                if (course.ActualGrade != null && course.ActualGrade.AcademicTaskTypes.Any())
                {
                    var result = _engine.CalculateCourseGrade(course.ActualGrade, scale);
                    double gradePoint = _converter.GetNumericalGradePoint(result.RunningPercentage, scale);
                    
                    currentQualityPoints += (gradePoint * (course.Units ?? 0));
                    completedUnits += (course.Units ?? 0);
                }
                else
                {
                    // Course is not finished yet
                    remainingUnits += (course.Units ?? 0);
                }
            }

            double totalUnits = completedUnits + remainingUnits;
            double targetQualityPoints = targetTermGWA * totalUnits;
            
            double pointsNeeded = targetQualityPoints - currentQualityPoints;

            if (remainingUnits == 0) return new TargetPredictionResult { IsPossible = false, Message = "Term is already complete." };

            double requiredGWAForRemaining = pointsNeeded / remainingUnits;

            // In Philippine GWA, lower is better. If they need less than a 1.0, it's impossible.
            bool isPossible = scale == GradeScaleType.GWA ? (requiredGWAForRemaining >= 1.0) : (requiredGWAForRemaining <= 4.0);

            return new TargetPredictionResult
            {
                IsPossible = isPossible,
                RequiredAverage = Math.Round(requiredGWAForRemaining, 2),
                Message = $"You need to average {Math.Round(requiredGWAForRemaining, 2)} in your remaining courses."
            };
        }
    }
}