using Acadeno.Backend.Models;

namespace Acadeno.Backend.Core;

public class GenAveSimulator
{
    private readonly GradingEngine _gradingEngine = new();

    public double PredictRequiredScore(AcademicTask task, double targetPercentage)
    {
        if (task.MaxScore == 0) return 0;
        return targetPercentage / 100 * task.MaxScore;
    }

    public double PredictRequiredCategoryScore(Grade currentGrade, double targetCourseGrade)
    {
       double currentWeightedGrade = 0;
       double remainWeight = 0;

        foreach (var type in currentGrade.AcademicTaskTypes)
        {
            var tasks = type.AcademicTasks ?? new List<AcademicTask>();
            
            bool hasGradedTasks = tasks.Any(t => t.Score >= 0);
            bool hasUngradedTasks = tasks.Any(t => t.Score < 0);

            if (hasGradedTasks)
            {
                double categoryAve = _gradingEngine.CalculateCategoryGrade(type);
                currentWeightedGrade += categoryAve * (type.Weight / 100);
            }

            if (hasUngradedTasks)
            {
                remainWeight += type.Weight;
            }
        }

        if (remainWeight == 0) return 0;

        double scoreNeededFromRemaining = targetCourseGrade - currentWeightedGrade;

        if (scoreNeededFromRemaining <= 0) return 0;

        double requiredAverage = scoreNeededFromRemaining / (remainWeight / 100);
       
        return requiredAverage;
    }

    public Dictionary<string, double> SimulateTermGoal(Term term, double targetGWA)
    {
        var courseGoals = new Dictionary<string, double>();

        foreach (var course in term.Courses)
        {
            courseGoals.Add(course.Name ?? "Unnamed Course", targetGWA);
        }

        return courseGoals;
    }

    public bool IsGoalAttainable(User user, double target)
    {
        if (target < 1.0 || target > 5.0) return false;

        return true;
    }
}