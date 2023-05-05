using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendTestTask.Data.Models
{
    public class Node
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public int TreeID { get; set; }

        [JsonIgnore]
        public Tree Tree { get; set; }

        [JsonIgnore]
        public int? ParentNodeID { get; set; }

        [JsonIgnore]
        public Node ParentNode { get; set; }

        public virtual ICollection<Node> Children { get; set; }
    }
}
