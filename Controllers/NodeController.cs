using BackendTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    //TODO - Try adding a different route:
    //[Route("api.user.tree.node.[action]")]
    public class NodeController : ApiControllerBase
    {
        private readonly ITreeAndNodeRepository _repository;

        public NodeController(ITreeAndNodeRepository repository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string treeName, int parentNodeId, string nodeName)
        {
            return await TryCatchOperationAsync(() =>
            {
                return _repository.CreateNodeAsync(treeName, parentNodeId, nodeName);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string treeName, int nodeId)
        {
            return await TryCatchOperationAsync(() =>
            {
                return _repository.DeleteNodeAsync(treeName, nodeId);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Rename(string treeName, int nodeId, string newNodeName)
        {
            return await TryCatchOperationAsync(() =>
            {
                return _repository.RenameNodeAsync(treeName, nodeId, newNodeName);
            });
        }

    }
}
