using Acadeno.Backend.Models.Enums;

namespace Acadeno.Backend.Models;

public class User
{
    public string UserID { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public string Program { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Using the Enum we talked about
    public GradeScaleType PreferredScale { get; set; }

    public double TargetGenAve { get; set; }
}