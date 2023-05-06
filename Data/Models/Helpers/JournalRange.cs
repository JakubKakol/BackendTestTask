namespace BackendTestTask.Data.Models.Helpers
{
    public class JournalRange
    {
        public int Skip { get; set; }
        public int Count { get; set; }
        public List<JournalResult> Items { get; set; }
    }
}
