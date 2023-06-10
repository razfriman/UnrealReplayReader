namespace UnrealReplayReader.Exceptions;

public class ReplayException : Exception
{
    public ReplayException(string message) : base(message)
    {
    }

    public ReplayException(string message, Exception innerException) : base(message, innerException)
    {
    }
}