namespace ProductCatalog.Application.Services.Impl;

public sealed class NotFoundException(string message) : Exception(message)
{
}
