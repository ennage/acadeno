using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Models;

public class Calendar
{
    public int UserID { get; set; }

    public string Title { get; set; } = string.Empty;
    public List<Calendar> Event { get; set; } = new();
    public string? Description { get; set; }
    public string SelectedMonth { get; set; } = string.Empty;
    public int SelectedYear { get; set; }

    public int Year { get; set; }
    public Month Month { get; set; }
    public int Day { get; set; }
}