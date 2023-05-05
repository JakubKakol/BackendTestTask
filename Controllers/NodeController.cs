using BackendTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    //TODO - Try adding a different route:
    //[Route("api.user.tree.node.[action]")]
    public class NodeController : ApiControllerBase
    {
        private readonly ITreeAndNodeRepository _repository;

        public NodeController(ITreeAndNodeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Create(string treeName, int parentNodeId, string nodeName)
        {
            return TryCatchOperation(() =>
            {
                _repository.CreateNode(treeName, parentNodeId, nodeName);
            });
        }

        [HttpPost]
        public IActionResult Delete(string treeName, int nodeId)
        {
            return TryCatchOperation(() =>
            {
                _repository.DeleteNode(treeName, nodeId);
            });
        }

        [HttpPost]
        public IActionResult Rename(string treeName, int nodeId, string newNodeName)
        {
            return TryCatchOperation(() =>
            {
                _repository.RenameNode(treeName, nodeId, newNodeName);
            });
        }

    }
}
