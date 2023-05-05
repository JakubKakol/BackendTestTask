namespace BackendTestTask.Data.Models
{
    public class Tree
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Node> Nodes { get; set; }
    }
}
