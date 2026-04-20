using Acadeno.Models.Enums;

namespace Acadeno.Models;

public class User
{
    // Primary Key for the Database
    public int Id { get; set; }

    // Your UML attributes converted to PascalCase Properties
    public string UserID { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public string Program { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Using the Enum we talked about
    public GradeScaleType PreferredScale { get; set; }

    public double TargetGenAve { get; set; }
}