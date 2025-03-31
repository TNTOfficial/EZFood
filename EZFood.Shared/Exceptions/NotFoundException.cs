namespace EZFood.Shared.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}