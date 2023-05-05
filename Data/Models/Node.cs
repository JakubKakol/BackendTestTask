using System.ComponentModel.DataAnnotations;

namespace BackendTestTask.Data.Models
{
    public class Node
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int TreeID { get; set; }

        public Tree Tree { get; set; }

        public int? ParentNodeID { get; set; }

        public Node ParentNode { get; set; }

        public virtual ICollection<Node> ChildrenNodes { get; set; }
    }
}
