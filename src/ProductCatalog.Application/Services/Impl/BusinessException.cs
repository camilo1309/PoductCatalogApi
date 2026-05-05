namespace ProductCatalog.Application.Services.Impl;

public sealed class BusinessException(string message) : Exception(message)
{
}
