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

        public async Task<JournalRange> GetRangeAsync(int skip, int take, JournalFilter filter)
        {
            IQueryable<JournalItem> journalItems;

            if (filter == null)
            {
                journalItems = _context.JournalItem.Take(take).Skip(skip);
            }
            else
            {
                journalItems = _context.JournalItem
                    .Where(j => (filter.From == null || filter.To == null) || (j.CreatedAt >= filter.From && j.CreatedAt <= filter.To))
                    .Where(j => string.IsNullOrEmpty(filter.Search) || j.ExceptionMessage.Contains(filter.Search) || j.StackTrace.Contains(filter.Search))
                    .Take(take).Skip(skip);
            }

            return new JournalRange
            {
                Skip = skip,
                Count = take,
                Items = await journalItems.Select(i => new JournalResult(i)).ToListAsync()
            };
        }

        public async Task<DetailedJournalResult> GetSingleAsync(int id)
        {
            var journalItem = await _context.JournalItem.FirstOrDefaultAsync(j => j.ID == id);

            if (journalItem == null)
            {
                throw new JournalItemNotFoundException(id);
            }

            return new DetailedJournalResult(journalItem);
        }
    }

    public interface IJournalRepository
    {
        Task<DetailedJournalResult> GetSingleAsync(int id);
        Task<JournalRange> GetRangeAsync(int skip, int take, JournalFilter filter);
    }
}
