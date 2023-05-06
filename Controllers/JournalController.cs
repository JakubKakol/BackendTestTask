using BackendTestTask.Data.DbContexts;
using BackendTestTask.Data.Models.Helpers;
using BackendTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    public class JournalController : ApiControllerBase
    {
        private readonly IJournalRepository _journalRepository;

        public JournalController(IJournalRepository journalRepository, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context) : base(httpContextAccessor, context)
        {
            _journalRepository = journalRepository;
        }

        [HttpPost]
        public async Task<IActionResult> GetRange(int skip, int take, [FromBody] JournalFilter filter)
        {
            return await TryCatchResultAsync(() =>
            {
                return _journalRepository.GetRangeAsync(skip, take, filter);
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetSingle(int id)
        {
            return await TryCatchResultAsync(() =>
            {
                return _journalRepository.GetSingleAsync(id);
            });
        }

    }
}
