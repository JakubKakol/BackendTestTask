using BackendTestTask.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTestTask.Controllers
{
    public class TreeController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TreeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string name)
        {
            try
            {
                var tree = _context.Tree
                    .Include(t => t.Children)
                    .First(t => t.Name == name);

                return Ok(tree);
            }
            catch (Exception ex)
            {
                //TODO - Add custom exception and handling
                return Json(ex);
            }
        }
    }
}
