using System.ComponentModel.DataAnnotations;

namespace BackendTestTask.Data.Models
{
    public class Tree
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Node> Children { get; set; }
    }
}
