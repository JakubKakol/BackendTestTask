using BackendTestTask.Data.DbContexts;
using BackendTestTask.Data.Models;
using BackendTestTask.Exceptions;
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
        {
            var tree = await _context.Tree.Include(d => d.Children).FirstOrDefaultAsync(t => t.Name == treeName);

            if (tree == null)
            {
                await CreateTreeAsync(treeName);
                return await GetTreeAsync(treeName);
            }

            return tree;
        }

        public async Task CreateTreeAsync(string treeName)
        {
            var tree = new Tree
            {
                Name = treeName
            };

            await _context.Tree.AddAsync(tree);
            await _context.SaveChangesAsync();
        }

        public async Task<Node> GetNodeFromTreeAsync(int treeId, int nodeID)
        {
            var node = await _context.Node.Where(n => n.TreeID == treeId).FirstOrDefaultAsync(n => n.ID == nodeID);

            if (node == null)
            {
                throw new NodeNotFoundException(treeId, nodeID);
            }

            return node;
        }

        public async Task CreateNodeAsync(string treeName, int parentNodeId, string nodeName)
        {
            var tree = await GetTreeAsync(treeName);
            var parentNode = await GetNodeFromTreeAsync(tree.ID, parentNodeId);

            if (tree.Children.Any(n => n.Name == nodeName))
            {
                throw new SiblingNodeNameDuplicateException(tree, nodeName);
            }

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
        Task CreateTreeAsync(string treeName);
        Task<Node> GetNodeFromTreeAsync(int treeId, int nodeID);
        Task CreateNodeAsync(string treeName, int parentNodeId, string nodeName);
        Task DeleteNodeAsync(string treeName, int nodeId);
        Task RenameNodeAsync(string treeName, int nodeId, string newNodeName);
    }
}
