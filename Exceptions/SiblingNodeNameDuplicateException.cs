using BackendTestTask.Data.Models;

namespace BackendTestTask.Exceptions
{
    public class SiblingNodeNameDuplicateException : SecureException
    {
        public SiblingNodeNameDuplicateException(Tree tree, string nodeName) : base($"Node with name {nodeName} already exists in {tree.Name} tree.")
        { }
    }
}
