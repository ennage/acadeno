using System.ComponentModel.DataAnnotations;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Models;

public class CalendarEntry
{
    [Key]
    public Guid CalendarID {get;set;} = Guid.NewGuid();

    public int UserID {get;set;}
    public string Title {get;set;} = string.Empty;
    public string? Description {get;set;}

    public string? Time { get; set; } // e.g., "9:00 AM - 10:00 AM"
    public bool IsAllDay { get; set; } = true;

    public int Year {get;set;}
    public Month Month {get;set;}
    public int Day {get;set;}

    public DateTime Date => new DateTime(Year, (int)Month, Day);

    public List<CalendarEntry> Event {get;set;} = new();
    public string SelectedMonth {get;set;} = string.Empty;
    public int SelectedYear {get;set;}

    
}