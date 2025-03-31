namespace EZFood.Shared.Exceptions;

public class UnauthorizedException(string message) : Exception(message)
{
}

public class ValidationException(string message) : Exception(message)
{
}