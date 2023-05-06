namespace BackendTestTask.Data.Models.Helpers
{
    public class JournalFilter
    {
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public string Search { get; set; }
    }
}
