using BackendTestTask.Data.DbContexts;
using BackendTestTask.Data.Models;
using BackendTestTask.Data.Models.Helpers;
using BackendTestTask.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackendTestTask.Services
{
    public class JournalRepository : IJournalRepository
    {
        private readonly ApplicationDbContext _context;

        public JournalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JournalResult>> GetRangeAsync(int skip, int take, JournalFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<JournalResult> GetSingleAsync(int id)
        {
            var journalItem = await _context.JournalItem.FirstOrDefaultAsync(j => j.ID == id);

            if (journalItem == null)
            {
                throw new JournalItemNotFoundException(id);
            }

            return new JournalResult(journalItem);
        }
    }

    public interface IJournalRepository
    {
        Task<JournalResult> GetSingleAsync(int id);
        Task<IEnumerable<JournalResult>> GetRangeAsync(int skip, int take, JournalFilter filter);
    }
}
