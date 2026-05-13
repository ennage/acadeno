using Acadeno.Backend.DTOs;
using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Simulation
{
    public class GradingEngine
    {
        private readonly GradeConverter _converter;

        public GradingEngine(GradeConverter converter)
        {
            _converter = converter;
        }
        
        // 1. TASK LEVEL
        public double CalculateTaskPercentage(AcademicTask task)
        {
            if (task.MaxScore == 0) return 0;
            return (double)task.Score / task.MaxScore * 100;
        }

        // 2. CATEGORY LEVEL
        public double CalculateCategoryGrade(AcademicTaskType type)
        {
            var gradedTasks = type.AcademicTasks?.Where(t => t.Score >= 0).ToList();      
            if (gradedTasks == null || !gradedTasks.Any()) return 0;

            double totalScore = gradedTasks.Sum(t => (double)t.Score);
            double totalMaxScore = gradedTasks.Sum(t => (double)t.MaxScore);

            if (totalMaxScore == 0) return 0;

            return (totalScore / totalMaxScore) * 100;
        }

        // 3. COURSE LEVEL
        public CourseGradeResult CalculateCourseGrade(Grade grade, GradeScaleType userScale)
        {
            if (grade.AcademicTaskTypes == null || !grade.AcademicTaskTypes.Any()) 
                return new CourseGradeResult();

            double totalWeightedGrade = 0;
            double definedWeight = 0;

            foreach (var type in grade.AcademicTaskTypes)
            {
                if (type.AcademicTasks != null && type.AcademicTasks.Any(t => t.Score >= 0))
                {
                    double categoryPercentage = CalculateCategoryGrade(type);
                    totalWeightedGrade += categoryPercentage * (type.Weight / 100);
                    definedWeight += type.Weight;
                }
            }

            definedWeight = Math.Min(definedWeight, 100);
            double unallocatedWeight = 100 - definedWeight;

            // Calculate the Raw Percentages
            double runningPercentage = definedWeight > 0 ? (totalWeightedGrade / (definedWeight / 100)) : 0;
            double floorPercentage = totalWeightedGrade; 

            // Return the bundled result with pre-formatted strings for UI
            return new CourseGradeResult
            {
                DefinedWeight = definedWeight,
                UnallocatedWeight = unallocatedWeight,
                RunningPercentage = Math.Round(runningPercentage, 2),
                FloorPercentage = Math.Round(floorPercentage, 2),
                
                // Formats the grade dynamically based on user settings
                DisplayRunningGrade = _converter.FormatGrade(runningPercentage, userScale),
                DisplayFloorGrade = _converter.FormatGrade(floorPercentage, userScale)
            };
        }

        // 4. TERM LEVEL 
        public double CalculateTermGradePoints(Term term, GradeScaleType userScale)
        {
            var activeCourses = term.Courses?.Where(c => c.ActualGrade != null && (c.Units ?? 0) > 0).ToList(); 
            if (activeCourses == null || !activeCourses.Any()) return 0;

            double totalQualityPoints = 0;
            double totalUnits = 0;

            foreach (var course in activeCourses)
            {
                var courseResult = CalculateCourseGrade(course.ActualGrade, userScale);
                
                // The raw numerical grade point (e.g. 1.25 or 3.8) to do the Term math
                double gradePoint = _converter.GetNumericalGradePoint(courseResult.RunningPercentage, userScale);
                
                totalQualityPoints += (gradePoint * (course.Units ?? 0));
                totalUnits += (course.Units ?? 0);
            }

            return totalUnits == 0 ? 0 : Math.Round(totalQualityPoints / totalUnits, 2);
        }

        public AcademicStandingResult GetOverallStanding(List<Term> terms, GradeScaleType scale)
        {
            var result = new AcademicStandingResult();
            if (terms == null || !terms.Any()) return result;

            double totalQualityPoints = 0;
            double totalUnits = 0;
            
            // Calculate Cumulative Stats
            foreach (var term in terms)
            {
                var activeCourses = term.Courses?.Where(c => c.ActualGrade != null && (c.Units ?? 0) > 0).ToList();
                if (activeCourses == null) continue;

                foreach (var course in activeCourses)
                {
                    var courseResult = CalculateCourseGrade(course.ActualGrade, scale);
                    double gradePoint = _converter.GetNumericalGradePoint(courseResult.RunningPercentage, scale);
                    
                    totalQualityPoints += (gradePoint * (course.Units ?? 0));
                    totalUnits += (course.Units ?? 0);
                }
            }

            // Set Cumulative Values
            result.CumulativeGwa = totalUnits > 0 ? Math.Round(totalQualityPoints / totalUnits, 2) : 0;
            
            // Get Current Term (Assuming last term is current)
            var latestTerm = terms.LastOrDefault();
            if (latestTerm != null)
            {
                result.CurrentTermGwa = CalculateTermGradePoints(latestTerm, scale);
            }

            // Map Standing & UI Helpers
            (result.Standing, result.StandingColor) = DetermineStanding(result.CumulativeGwa);
            
            // Circle Percentage Logic: (Max - Actual) / (Max - Min) * 100
            // Assuming 1.0 is the best and 3.0 is the passing limit
            result.CirclePercentage = Math.Clamp(((3.0 - result.CumulativeGwa) / (3.0 - 1.0)) * 100, 0, 100);

            return result;
        }

        private (string, string) DetermineStanding(double gwa)
        {
            if (gwa <= 0 || gwa > 3.0) return ("Incomplete", "#94a3b8");
            if (gwa <= 1.25) return ("Excellent", "#10b981");
            if (gwa <= 1.75) return ("Very Good", "#3b82f6");
            return ("Satisfactory", "#f59e0b");
        }
    }
}