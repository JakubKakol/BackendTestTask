using BackendTestTask.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    public class JournalController : ApiControllerBase
    {

        public JournalController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context) : base(httpContextAccessor, context)
        { }

        [HttpPost]
        public async Task<IActionResult> GetSingle(int id)
        {
            return await TryCatchResultAsync(() =>
            {

            });
        }

    }
}
