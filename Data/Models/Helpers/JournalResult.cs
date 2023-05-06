namespace BackendTestTask.Data.Models.Helpers
{
    public class JournalResult
    {
        public JournalResult()
        { }

        public JournalResult(JournalItem item)
        {
            Text = $"RequestID = {item.EventId}" +
                    $"\r\nRequestQueryString = {item.RequestQueryString}" +
                    $"\r\nRequestBody = {item.RequestBody}" +
                    $"\r\nExceptionMessage = {item.ExceptionMessage}" +
                    $"\r\nStackTrace = {item.StackTrace}";
            Id = item.ID;
            EventId = item.EventId;
            CreatedAt = item.CreatedAt;
        }

        public string Text { get; set; }
        public int Id { get; set; }
        public string EventId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
