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

        public Task<IEnumerable<JournalItem>> GetRange(int skip, int take, JournalFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<JournalItem> GetSingle(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IJournalRepository
    {
        Task<JournalItem> GetSingle(int id);
        Task<IEnumerable<JournalItem>> GetRange(int skip, int take, JournalFilter filter);
    }
}
