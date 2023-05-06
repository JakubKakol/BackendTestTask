using BackendTestTask.Data.Models;

namespace BackendTestTask.Exceptions
{
    public class JournalItemNotFoundException : SecureException
    {
        public JournalItemNotFoundException(int id) : base($"Journal item with ID = {id} was not found.")
        { }
    }
}
