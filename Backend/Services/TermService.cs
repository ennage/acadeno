using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;

namespace Acadeno.Backend.Services
{
    public class TermService
    {
        private readonly AppDbContext _db;

        public TermService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Term>> GetAvailableTermsAsync()
        {
            return await _db.Terms
                .AsNoTracking()
                .ToListAsync();
        }

        // Fetch terms exclusively for the selected Academic Year
        public async Task<List<Term>> GetTermsByYearAsync(string yearId)
        {
            return await _db.Terms
                .AsNoTracking()
                .Where(t => t.YearID == yearId)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<bool> AddTermAsync(Term term)
        {
            if (string.IsNullOrEmpty(term.TermID))
            {
                term.TermID = Guid.NewGuid().ToString();
            }

            if (term.IsCurrent)
            {
                await UnsetOtherCurrentTermsAsync(term.YearID);
            }

            await _db.Terms.AddAsync(term);
            return await _db.SaveChangesAsync() > 0;
        }

        // Internal helper to ensure only ONE term is "Current" per Academic Year
        private async Task UnsetOtherCurrentTermsAsync(string yearId)
        {
            var activeTerms = await _db.Terms
                .Where(t => t.YearID == yearId && t.IsCurrent)
                .ToListAsync();

            // Turn off the IsCurrent flag for any existing terms in this specific year
            foreach (var t in activeTerms)
            {
                t.IsCurrent = false;
            }

            // Save changes before the new term gets added
            if (activeTerms.Any())
            {
                _db.Terms.UpdateRange(activeTerms);
            }
        }
    }
}