using Acadeno.Backend.DTOs;
using Acadeno.Backend.Enums;
using Acadeno.Backend.Simulation;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _db;
        private readonly GradeConverter _converter;
        private readonly GradingEngine _engine;

        public DashboardService(AppDbContext db, GradeConverter converter, GradingEngine engine)
        {
            _db = db;
            _converter = converter;
            _engine = engine;
        }
        
        public string GetAcademicStanding(double gwa)
        {
            // Note: In PH GWA, 0 usually means no grades yet.
            if (gwa <= 0) return "No Data";
            if (gwa >= 1.00 && gwa <= 1.25) return "President Lister";
            if (gwa > 1.25 && gwa <= 1.75) return "Dean's Lister";
            if (gwa <= 2.25) return "Satisfactory";
            if (gwa <= 3.00) return "Passing";
            return "Failing";
        }

        public async Task<DashboardStats> GetDashboardStatsAsync(string userId)
        {
            // 1. Get the User's Preferred Scale and info
            var user = await _db.Users.FindAsync(userId);
            var scale = user?.PreferredScale ?? GradeScaleType.GWA;
            var universityName = string.IsNullOrWhiteSpace(user?.University) ? "UNIVERSITY NOT SET" : user.University;

            // 2. Fetch the Active Term for "Term GPA" and "Units Loaded"
            var activeTerm = await _db.Terms
                .Include(t => t.Courses)
                    .ThenInclude(c => c.ActualGrade)
                        .ThenInclude(g => g.AcademicTaskTypes)
                            .ThenInclude(type => type.AcademicTasks)
                .FirstOrDefaultAsync(t => t.IsCurrent && t.AcademicYear.UserID == userId);

            double termGradePoints = 0;
            double unitsLoaded = 0;

            if (activeTerm != null && activeTerm.Courses != null)
            {
                // Let the Engine do the heavy lifting!
                termGradePoints = _engine.CalculateTermGradePoints(activeTerm, scale);
                unitsLoaded = activeTerm.Courses.Sum(c => c.Units ?? 0);
            }

            // 3. Fetch ALL Courses for "Current GWA" (Cumulative)
            var allCourses = await _db.Courses
                .Include(c => c.ActualGrade)
                    .ThenInclude(g => g.AcademicTaskTypes)
                        .ThenInclude(type => type.AcademicTasks)
                .Where(c => c.UserID == userId)
                .ToListAsync();

            double totalQualityPoints = 0;
            double totalCumulativeUnits = 0;

            foreach (var course in allCourses)
            {
                if (course.ActualGrade != null && (course.Units ?? 0) > 0)
                {
                    var courseResult = _engine.CalculateCourseGrade(course.ActualGrade, scale);
                    double gradePoint = _converter.GetNumericalGradePoint(courseResult.RunningPercentage, scale);

                    totalQualityPoints += gradePoint * (course.Units ?? 0);
                    totalCumulativeUnits += course.Units ?? 0;
                }
            }

            double cumulativeGradePoints = totalCumulativeUnits > 0 ? (totalQualityPoints / totalCumulativeUnits) : 0;

            // 4. Return the beautifully formatted data to the UI
            return new DashboardStats
            {
                CurrentGWA = cumulativeGradePoints > 0 ? cumulativeGradePoints.ToString("0.00") : "N/A",
                TermGPA = termGradePoints > 0 ? termGradePoints.ToString("0.00") : "N/A",
                UnitsLoaded = unitsLoaded,
                AcademicStanding = GetAcademicStanding(termGradePoints > 0 ? termGradePoints : cumulativeGradePoints)
            };
        }
    }

    // A clean data container just for your UI
    
}