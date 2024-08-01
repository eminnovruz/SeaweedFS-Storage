namespace FileServer_Asp.CustomExceptions;

public class ExistingAssignException : Exception
{
    public ExistingAssignException(string message) : base(message)
    {
    }
}
