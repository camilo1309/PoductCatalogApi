namespace ProductCatalog.Application.Services;

public sealed class NotFoundException(string message) : Exception(message)
{
}
