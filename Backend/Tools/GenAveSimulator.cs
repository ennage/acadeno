using Acadeno.Backend.Models;

namespace Acadeno.Backend.Tools;

public class GenAveSimulator
{
    private readonly GradingEngine _gradingEngine = new();

    public double PredictReqiredScore(AcademicTask task, double targetPercentage)
    {
        if (task.MaxScore == 0) return 0;
        return (targetPercentage / 100) * task.MaxScore;
    }

    public Dictionary<string, double> SimulateTermGoal(Term term, double target)
    {
        var courseGoals = new Dictionary<string, double>();
        courseGoals.Add("Required Course Ave", target);

        return courseGoals;
    }

    public bool IsGoalAttainable(User user, double target)
    {
        if (target < 1.0 || target > 5.0) return false;

        return true;
    }
}