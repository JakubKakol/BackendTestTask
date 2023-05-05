namespace BackendTestTask.Exceptions
{
    public class SecureException : Exception
    {
        public SecureException() : base("Secure exception was thrown.")
        { }

        public SecureException(string message) : base(message)
        { }
    }
}
