using System.ComponentModel.DataAnnotations;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Models;

public class CalendarEntry
{
    [Key]
    public Guid CalendarID {get;set;} = Guid.NewGuid();

    public string Title {get;set;} = string.Empty;
    public string? Description {get;set;}

    public string? Time  {get; set;}
    public bool IsAllDay {get; set;} = true;

    public int Day {get;set;}
    public int SelectedYear {get;set;}
    public int Year {get;set;}
    public string SelectedMonth {get;set;} = string.Empty;
    public Month Month {get;set;}
    public DateTime Date => new DateTime(Year, (int)Month, Day);

    // Foreign Key
    public Guid UserID {get;set;}

    // Holds Calendar events?
    public List<CalendarEntry> Event {get;set;} = new();    
}