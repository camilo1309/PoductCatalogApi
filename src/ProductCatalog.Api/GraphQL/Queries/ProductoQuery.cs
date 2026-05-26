using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Services;

namespace ProductCatalog.Api.GraphQL.Queries;

public sealed class ProductoQuery
{
    public async Task<IReadOnlyList<ProductoDto>> GetProductos(
        [Service] IProductoService productoService,
        CancellationToken cancellationToken)
    {
        return await productoService.GetAllAsync(cancellationToken);
    }

    public async Task<ProductoDto> GetProductoById(
        int id,
        [Service] IProductoService productoService,
        CancellationToken cancellationToken)
    {
        return await productoService.GetByIdAsync(id, cancellationToken);
    }
}
