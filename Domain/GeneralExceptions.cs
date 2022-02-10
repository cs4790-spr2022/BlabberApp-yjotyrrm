namespace Domain;
public class InvalidUsernameException : System.Exception
{
    public InvalidUsernameException() { }
    public InvalidUsernameException(string message) : base(message) { }
    public InvalidUsernameException(string message, System.Exception inner) : base(message, inner) { }
    protected InvalidUsernameException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

public class InvalidContentException : System.Exception
{
    public InvalidContentException() { }
    public InvalidContentException(string message) : base(message) { }
    public InvalidContentException(string message, System.Exception inner) : base(message, inner) { }
    protected InvalidContentException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}