using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Services;
public class CalendarService
{
    private readonly AppDbContext _db;

    public CalendarService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CalendarEntry> GetCalendarView(int year, Month month, int userId)
    {
        var events = await _db.Calendars
            .Where(e => e.UserID == userId && e.Year == year && e.Month == month)
            .OrderBy(e => e.Day)
            .ToListAsync();
        
        //  Calculate "Start and End" logic (The Calendar Math)
        DateTime firstOfMonth = new DateTime(year, (int)month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, (int)month);

        // This tells the UI which day of the week to start drawing (0 = Sunday)
        int startDayOfWeek = (int)firstOfMonth.DayOfWeek;

        return new CalendarEntry
        {
            Title = $"{month.ToString().ToUpper()} {year}",
            Event = events,
            SelectedMonth = month.ToString(),
            SelectedYear = year,
            Year = year,
            Month = month,
            Day = daysInMonth
        };
    }

    public async Task<bool> AddCalendarEvent(CalendarEntry newEvent)
    {
        // Ensure the ID is generated if not already set
        if (newEvent.CalendarID == Guid.Empty)
        {
            newEvent.CalendarID = Guid.NewGuid();
        }
        _db.Calendars.Add(newEvent);
        var result =await _db.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCalendarEvent(Guid calendarId)
    {
        var item = await _db.Calendars.FindAsync(calendarId);
        if (item == null) return false;

        _db.Calendars.Remove(item);
        return await _db.SaveChangesAsync() > 0;
    }
}