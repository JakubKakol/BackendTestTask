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

        public Node GetNodeFromTree(Tree tree, int nodeID)
            => tree.Children.First(n => n.ID == nodeID);

        public Tree GetTree(string treeName)
            => _context.Tree.Include(d => d.Children).First(t => t.Name == treeName);

        public void CreateNode(string treeName, int parentNodeId, string nodeName)
        {
            var tree = GetTree(treeName);
            var parentNode = GetNodeFromTree(tree, parentNodeId);

            var newNode = new Node
            {
                Name = nodeName,
                ParentNodeID = parentNode.ID,
                TreeID = tree.ID
            };

            _context.Node.Add(newNode);
            _context.SaveChanges();
        }

        public void DeleteNode(string treeName, int nodeId)
        {
            var tree = GetTree(treeName);
            var node = GetNodeFromTree(tree, nodeId);

            _context.Node.Remove(node);
            _context.SaveChanges();
        }

        public void RenameNode(string treeName, int nodeId, string newNodeName)
        {
            var tree = GetTree(treeName);
            var node = GetNodeFromTree(tree, nodeId);

            node.Name = newNodeName;
            _context.Node.Update(node);
            _context.SaveChanges();
        }
    }

    public interface ITreeAndNodeRepository
    {
        Tree GetTree(string treeName);
        Node GetNodeFromTree(Tree tree, int nodeID);
        void CreateNode(string treeName, int parentNodeId, string nodeName);
        void DeleteNode(string treeName, int nodeId);
        void RenameNode(string treeName, int nodeId, string newNodeName);
    }
}
