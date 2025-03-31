namespace EZFood.Shared.Exceptions;

public class EZFoodException : Exception
{
    public string[] Details { get; }

    public EZFoodException(string message) : base(message)
    {
        Details = Array.Empty<string>();
    }

    public EZFoodException(string message, string[] details) : base(message)
    {
        Details = details;
    }
}