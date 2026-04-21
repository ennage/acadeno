using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models;

public class AcademicTask
{
    public string CourseID { get; set; } = string.Empty;
    public double Score { get; set; }
    public double MaxScore { get; set; }

    public void SetScore(double score) => Score = score;
    public void SetMaxScore(double maxScore) => MaxScore = maxScore;
}