using BackendTestTask.Data.DbContexts;
using BackendTestTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendTestTask.Services
{
    public class TreeAndNodeRepository : ITreeAndNodeRepository
    {
        private readonly ApplicationDbContext _context;

        public TreeAndNodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tree> GetTreeAsync(string treeName)
            => await _context.Tree.Include(d => d.Children).FirstAsync(t => t.Name == treeName);

        public async Task<Node> GetNodeFromTreeAsync(int treeId, int nodeID)
            => await _context.Node.Where(n => n.TreeID == treeId).FirstAsync(n => n.ID == nodeID);

        public async Task CreateNodeAsync(string treeName, int parentNodeId, string nodeName)
        {
            var tree = await GetTreeAsync(treeName);
            var parentNode = await GetNodeFromTreeAsync(tree.ID, parentNodeId);

            var newNode = new Node
            {
                Name = nodeName,
                ParentNodeID = parentNode.ID,
                TreeID = tree.ID
            };

            await _context.Node.AddAsync(newNode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNodeAsync(string treeName, int nodeId)
        {
            var tree = await GetTreeAsync(treeName);
            var node = await GetNodeFromTreeAsync(tree.ID, nodeId);

            _context.Node.Remove(node);
            await _context.SaveChangesAsync();
        }

        public async Task RenameNodeAsync(string treeName, int nodeId, string newNodeName)
        {
            var tree = await GetTreeAsync(treeName);
            var node = await GetNodeFromTreeAsync(tree.ID, nodeId);

            node.Name = newNodeName;
            _context.Node.Update(node);
            await _context.SaveChangesAsync();
        }
    }

    public interface ITreeAndNodeRepository
    {
        Task<Tree> GetTreeAsync(string treeName);
        Task<Node> GetNodeFromTreeAsync(int treeId, int nodeID);
        Task CreateNodeAsync(string treeName, int parentNodeId, string nodeName);
        Task DeleteNodeAsync(string treeName, int nodeId);
        Task RenameNodeAsync(string treeName, int nodeId, string newNodeName);
    }
}
