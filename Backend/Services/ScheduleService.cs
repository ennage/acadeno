using Microsoft.EntityFrameworkCore;
using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class ScheduleService
    {
        private readonly AppDbContext _db;

        public ScheduleService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ScheduleEntry>> GetWeeklySchedule(Guid userId)
        {
            return await _db.ScheduleEntries
                .Where(e => e.UserID == userId.ToString())
                .OrderBy(e => e.Day)
                .ThenBy(e => e.StartTime)
                .ToListAsync();
        }

        public async Task<bool> CheckScheduleConflict(ScheduleEntry newEntry)
        {
            var existingEntries = await _db.ScheduleEntries
                .Where(e => e.UserID == newEntry.UserID && e.Day == newEntry.Day)
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

        public async Task<string> CalculateLoad(Guid userId)
        {
            var result = await _db.ScheduleEntries
                .Where(e => e.UserID == userId.ToString())
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