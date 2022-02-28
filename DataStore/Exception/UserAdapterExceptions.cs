namespace DataStore.exception
{
    public class UserAdapterException : System.Exception
    {
        public UserAdapterException() { }
        public UserAdapterException(string message) : base(message) { }
        public UserAdapterException(string message, System.Exception inner) : base(message, inner) { }
        protected UserAdapterException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
