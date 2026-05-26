using ProductCatalog.Api.GraphQL.Inputs;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Services;

namespace ProductCatalog.Api.GraphQL.Mutations;

public sealed class ProductoMutation
{
    public async Task<ProductoDto> CreateProducto(
        CreateProductoInput input,
        [Service] IProductoService productoService,
        CancellationToken cancellationToken)
    {
        var request = new CreateProductoRequest
        {
            Nombre = input.Nombre,
            Descripcion = input.Descripcion,
            Precio = input.Precio
        };

        return await productoService.CreateAsync(request, cancellationToken);
    }

    public async Task<ProductoDto> UpdateProducto(
        UpdateProductoInput input,
        [Service] IProductoService productoService,
        CancellationToken cancellationToken)
    {
        var request = new UpdateProductoRequest
        {
            Nombre = input.Nombre,
            Descripcion = input.Descripcion,
            Precio = input.Precio
        };

        return await productoService.UpdateAsync(input.Id, request, cancellationToken);
    }

    public async Task<bool> DeleteProducto(
        int id,
        [Service] IProductoService productoService,
        CancellationToken cancellationToken)
    {
        await productoService.DeleteAsync(id, cancellationToken);
        return true;
    }
}
