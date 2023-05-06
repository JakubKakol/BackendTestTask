namespace BackendTestTask.Data.Models.Helpers
{
    public class DetailedJournalResult : JournalResult
    {
        public DetailedJournalResult()
        { }

        public DetailedJournalResult(JournalItem item) : base(item)
        {
            Text = $"RequestID = {item.EventId}" +
                    $"\r\nRequestQueryString = {item.RequestQueryString}" +
                    $"\r\nRequestBody = {item.RequestBody}" +
                    $"\r\nExceptionMessage = {item.ExceptionMessage}" +
                    $"\r\nStackTrace = {item.StackTrace}";
        }

        public string Text { get; set; }
    }
}
