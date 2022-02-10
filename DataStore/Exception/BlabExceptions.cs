namespace DataStore.exception;
public class BlabException : System.Exception
{

    public BlabException(string message) : base(message) { }
    public BlabException(string message, System.Exception inner) : base(message, inner) { }

}

public class BlabAdapterNotFoundException: Exception 
{
    public BlabAdapterNotFoundException(string message) : base(message) { }
    public BlabAdapterNotFoundException(string message, System.Exception inner) : base(message, inner) { }
}