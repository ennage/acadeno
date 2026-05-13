using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class YearService
    {
        private readonly AppDbContext _db;

        public YearService(AppDbContext db)
        {
            _db = db;
        }

        // Fetch years for the specific user
        public async Task<List<AcademicYear>> GetYearsByUserAsync(string userId)
        {
            return await _db.AcademicYears
                .AsNoTracking()
                .Where(y => y.UserID == userId)
                // Sort alphabetically descending (e.g., "2025-2026" comes before "2024-2025")
                .OrderByDescending(y => y.YearSpan) 
                .ToListAsync();
        }

        // Add a new Academic Year safely
        public async Task<bool> AddAcademicYearAsync(AcademicYear year)
        {
            if (string.IsNullOrEmpty(year.YearID))
            {
                year.YearID = Guid.NewGuid().ToString();
            }

            // If the user marks this new year as current, unset the others
            if (year.IsCurrent)
            {
                await UnsetOtherCurrentYearsAsync(year.UserID);
            }

            await _db.AcademicYears.AddAsync(year);
            return await _db.SaveChangesAsync() > 0;
        }

        // Update an existing year to be the "Active" one
        public async Task<bool> SetAsCurrentYearAsync(string userId, string yearId)
        {
            await UnsetOtherCurrentYearsAsync(userId);

            var targetYear = await _db.AcademicYears.FindAsync(yearId);
            if (targetYear != null)
            {
                targetYear.IsCurrent = true;
                _db.AcademicYears.Update(targetYear);
                return await _db.SaveChangesAsync() > 0;
            }
            return false;
        }

        // Internal helper to ensure only ONE year is "Current" at a time
        private async Task UnsetOtherCurrentYearsAsync(string userId)
        {
            var activeYears = await _db.AcademicYears
                .Where(y => y.UserID == userId && y.IsCurrent)
                .ToListAsync();

            foreach (var y in activeYears)
            {
                y.IsCurrent = false;
            }

            if (activeYears.Any())
            {
                _db.AcademicYears.UpdateRange(activeYears);
            }
        }
    }
}