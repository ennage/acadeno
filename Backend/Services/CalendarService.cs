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

    public async Task<CalendarEntry> GetCalendarView(int year, Month month, string userId)
    {
        var events = await _db.CalendarEntries
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
        if (newEvent.CalendarID == string.Empty)
        {
            newEvent.CalendarID = Guid.NewGuid().ToString();
        }
        _db.CalendarEntries.Add(newEvent);
        var result =await _db.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCalendarEvent(Guid calendarId)
    {
        var item = await _db.CalendarEntries.FindAsync(calendarId);
        if (item == null) return false;

        _db.CalendarEntries.Remove(item);
        return await _db.SaveChangesAsync() > 0;
    }
}