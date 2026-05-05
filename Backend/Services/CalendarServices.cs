using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Tools;
using Acadeno.Backend.Models;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Services;
public class CalendarServices
{
    private readonly AppDbContext _db;

    public CalendarServices(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Calendar> GetCalendarView(int year, Month month, int userId)
    {
        var events = await _db.Calendars
            .Where(e => e.UserID == userId && e.Year == year && e.Month == month)
            .OrderBy(e => e.Day)
            .ToListAsync();

        return new Calendar
        {
            Title = $"{month.ToString().ToUpper()} {year}",
            Event = events,
            SelectedMonth = month.ToString(),
            SelectedYear = year
        };
    }

    public async Task<Calendar> GetCalendarMonthView(int year, Month month, int userId)
    {
        return await GetCalendarView(year, month, userId);
    }

    public async Task<bool> AddCalendarEvent(Calendar newEvent)
    {

        _db.Calendars.Add(newEvent);
        var result =await _db.SaveChangesAsync();
        return result > 0;
    }
}