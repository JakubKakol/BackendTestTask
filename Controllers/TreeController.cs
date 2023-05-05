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

        [HttpPost]
        public IActionResult Get(string name)
        {
            return TryCatchResult(() =>
            {
                return _repository.GetTree(name);
            });
        }
    }
}
