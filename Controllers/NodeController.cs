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

        [HttpGet]
        public IActionResult Create(string treeName, int parentNodeId, string nodeName)
        {
            try
            {
                _repository.CreateNode(treeName, parentNodeId, nodeName);
            }
            catch (Exception ex)
            {
                //TODO - Add custom exception and handling
                return Json(ex);
            }

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string treeName, int nodeId)
        {
            try
            {
                _repository.DeleteNode(treeName, nodeId);
            }
            catch (Exception ex)
            {
                //TODO - Add custom exception and handling
                return Json(ex);
            }

            return Ok();
        }

        [HttpPatch]
        public IActionResult Rename(string treeName, int nodeId, string newNodeName)
        {
            try
            {
                _repository.RenameNode(treeName, nodeId, newNodeName);
            }
            catch (Exception ex)
            {
                //TODO - Add custom exception and handling
                return Json(ex);
            }

            return Ok();
        }

    }
}
