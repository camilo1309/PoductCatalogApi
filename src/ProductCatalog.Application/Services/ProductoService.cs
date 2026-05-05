using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Services;

public sealed class ProductoService(IProductoRepository productoRepository) : IProductoService
{
    public async Task<IReadOnlyList<ProductoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var productos = await productoRepository.GetAllAsync(cancellationToken);
        return productos.Select(ToDto).ToList();
    }

    public async Task<ProductoDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var producto = await productoRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"No existe un producto con id {id}.");

        return ToDto(producto);
    }

    public async Task<ProductoDto> CreateAsync(CreateProductoRequest request, CancellationToken cancellationToken = default)
    {
        if (await productoRepository.ExistsByNameAsync(request.Nombre, null, cancellationToken))
            throw new BusinessException("Ya existe un producto con el mismo nombre.");

        var producto = new Producto(request.Nombre, request.Descripcion, request.Precio);

        await productoRepository.AddAsync(producto, cancellationToken);
        await productoRepository.SaveChangesAsync(cancellationToken);

        return ToDto(producto);
    }

    public async Task<ProductoDto> UpdateAsync(int id, UpdateProductoRequest request, CancellationToken cancellationToken = default)
    {
        var producto = await productoRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"No existe un producto con id {id}.");

        if (await productoRepository.ExistsByNameAsync(request.Nombre, id, cancellationToken))
            throw new BusinessException("Ya existe otro producto con el mismo nombre.");

        producto.Actualizar(request.Nombre, request.Descripcion, request.Precio);

        productoRepository.Update(producto);
        await productoRepository.SaveChangesAsync(cancellationToken);

        return ToDto(producto);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var producto = await productoRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"No existe un producto con id {id}.");

        productoRepository.Delete(producto);
        await productoRepository.SaveChangesAsync(cancellationToken);
    }

    private static ProductoDto ToDto(Producto producto)
    {
        return new ProductoDto(
            producto.Id,
            producto.Nombre,
            producto.Descripcion,
            producto.Precio,
            producto.FechaCreacion,
            producto.FechaActualizacion
        );
    }
}
