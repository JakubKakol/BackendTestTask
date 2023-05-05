using BackendTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    public class TreeController : ApiControllerBase
    {
        private readonly ITreeAndNodeRepository _repository;

        public TreeController(ITreeAndNodeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get(string name)
        {
            try
            {
                var tree = _repository.GetTree(name);

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
