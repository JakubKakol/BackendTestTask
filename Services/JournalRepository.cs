using BackendTestTask.Data.DbContexts;
using BackendTestTask.Data.Models;
using BackendTestTask.Data.Models.Helpers;

namespace BackendTestTask.Services
{
    public class JournalRepository : IJournalRepository
    {
        private readonly ApplicationDbContext _context;

        public JournalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JournalItem>> GetRangeAsync(int skip, int take, JournalFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<JournalItem> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IJournalRepository
    {
        Task<JournalItem> GetSingleAsync(int id);
        Task<IEnumerable<JournalItem>> GetRangeAsync(int skip, int take, JournalFilter filter);
    }
}
