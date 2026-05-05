namespace ProductCatalog.Application.Services;

public sealed class BusinessException(string message) : Exception(message)
{
}
