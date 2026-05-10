using Acadeno.Backend.Models;
using Acadeno.Backend.Tools;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}