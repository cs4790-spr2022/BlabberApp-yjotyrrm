namespace DataStore.exception
{
    public class BlabAdapterException : System.Exception
    {

        public BlabAdapterException(string message) : base(message) { }
        public BlabAdapterException(string message, System.Exception inner) : base(message, inner) { }

    }

    public class BlabAdapterNotFoundException : Exception
    {
        public BlabAdapterNotFoundException(string message) : base(message) { }
        public BlabAdapterNotFoundException(string message, System.Exception inner) : base(message, inner) { }
    }
}
