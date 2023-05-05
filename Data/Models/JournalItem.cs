namespace BackendTestTask.Data.Models
{
    public class JournalItem
    {
        public int ID { get; set; }
        public string EventId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string RequestQueryString { get; set; }
        public string RequestBody { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
