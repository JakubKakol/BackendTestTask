using BackendTestTask.Data.DbContexts;
using BackendTestTask.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTestTask.Controllers
{
    //TODO - Try adding a different route:
    //[Route("api.user.tree.node.[action]")]
    public class NodeController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(string treeName, int parentNodeId, string nodeName)
        {
            try
            {
                var tree = _context.Tree.Include(d => d.Children).First(t => t.Name == treeName);
                var parentNode = tree.Children.First(n => n.ID == parentNodeId);

                var newNode = new Node
                {
                    Name = nodeName,
                    ParentNodeID = parentNode.ID,
                    TreeID = tree.ID
                };

                _context.Node.Add(newNode);
                _context.SaveChanges();
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
                var tree = _context.Tree.Include(d => d.Children).First(t => t.Name == treeName);
                var node = tree.Children.First(n => n.ID == nodeId);

                _context.Node.Remove(node);
                _context.SaveChanges();
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
                var tree = _context.Tree.Include(d => d.Children).First(t => t.Name == treeName);
                var node = tree.Children.First(n => n.ID == nodeId);

                node.Name = newNodeName;
                _context.Node.Update(node);
                _context.SaveChanges();
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
