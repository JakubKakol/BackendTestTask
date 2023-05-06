namespace BackendTestTask.Data.Models.Helpers
{
    public class JournalResult
    {
        public JournalResult()
        { }

        public JournalResult(JournalItem item)
        {
            Id = item.ID;
            EventId = item.EventId;
            CreatedAt = item.CreatedAt;
        }
        public int Id { get; set; }
        public string EventId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
