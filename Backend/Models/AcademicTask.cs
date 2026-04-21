using System.ComponentModel.DataAnnotations;

namespace Acadeno.Backend.Models;

public class AcademicTask
{
    [Key]
    public string CourseID { get; set; } = string.Empty;

    [Required]
    public double Score { get; set; }

    [Required]
    public double MaxScore { get; set; }

    public void SetScore(double score) => Score = score;
    public void SetMaxScore(double maxScore) => MaxScore = maxScore;
}