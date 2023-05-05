using BackendTestTask.Data.Models;

namespace BackendTestTask.Exceptions
{
    public class NodeNotFoundException : SecureException
    {
        public NodeNotFoundException(int treeId, int nodeID) : base($"Node with id {nodeID} was not found in tree with id {treeId}.")
        { }
    }
}
