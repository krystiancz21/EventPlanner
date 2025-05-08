namespace EventPlanner.Domain.Exceptions;

public class BadRequestException(string resourceType, string resourceIdentifier, string  message)
    : Exception($"{resourceType} with id: {resourceIdentifier} - bad request: {message}")
{
}