using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class ScheduleService
    {
        private readonly AppDbContext _db;

        public ScheduleService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ScheduleEntry>> GetWeeklySchedule(string userId)
        {
            return await _db.ScheduleEntries
                .Where(e => e.UserID == userId)
                .OrderBy(e => e.DayOfWeek)
                .ThenBy(e => e.StartTime)
                .ToListAsync();
        }

        public async Task<bool> CheckScheduleConflict(ScheduleEntry newEntry)
        {
            var existingEntries = await _db.ScheduleEntries
                .Where(e => e.UserID == newEntry.UserID && e.DayOfWeek == newEntry.DayOfWeek)
                .ToListAsync();

            foreach (var entry in existingEntries)
            {
                if (newEntry.StartTime < entry.EndTime && newEntry.EndTime > entry.StartTime)
                {
                    return true; 
                }
            }
            return false; 
        }

        public async Task<string> CalculateLoad(string userId)
        {
            var result = await _db.ScheduleEntries
                .Where(e => e.UserID == userId)
                .ToListAsync();

            double totalHours = 0;

            foreach (var entry in result)
            {
                var duration = entry.EndTime - entry.StartTime;
                totalHours += duration.TotalHours;
            }
    
            const double standardWeek = 40.0;
            double loadPercentage = totalHours / standardWeek * 100;

            return $"{Math.Round(loadPercentage)}% Load";
        }
    }
}