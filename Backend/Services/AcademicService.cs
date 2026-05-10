using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class AcademicService
    {
        private readonly AppDbContext _db;
       
        public AcademicService(AppDbContext db)
        {
            _db = db;
        }

        public string GetAcademicStanding(double gwa)
        {
            if (gwa >= 1.00 && gwa<= 1.25) return "President Lister";
            if (gwa > 1.25 && gwa <= 1.75) return "Dean's Lister";
            if (gwa <= 2.25) return "Satisfactory";
            if (gwa <= 3.00) return "Passing";
            return "Failing";
        }

        public async Task<object> GetStats(string userId)
        {
            var courses = await _db.Courses
                .Where(c => c.UserID == userId)
                .ToListAsync();

            double totalUnit = courses.Sum(c => c.Units.GetValueOrDefault(0));

            double weightedGwaSum = courses.Sum(c => (c.ActualGrade?.CourseGrade ?? 0) * c.Units.GetValueOrDefault(0));

            double currentGwa = (totalUnit > 0) ? weightedGwaSum / totalUnit : 0;

            return new
            {
                CurrentGWA = Math.Round(currentGwa, 2),
                UnitsLoaded = totalUnit,
                AcademicStanding = GetAcademicStanding(currentGwa)
            };
        }
    }
}