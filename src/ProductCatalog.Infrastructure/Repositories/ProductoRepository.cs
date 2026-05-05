using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Infrastructure.Repositories;

public sealed class ProductoRepository(AppDbContext context) : IProductoRepository
{
    public async Task<IReadOnlyList<Producto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Productos
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Producto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Productos
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Producto producto, CancellationToken cancellationToken = default)
    {
        await context.Productos.AddAsync(producto, cancellationToken);
    }

    public void Update(Producto producto)
    {
        context.Productos.Update(producto);
    }

    public void Delete(Producto producto)
    {
        context.Productos.Remove(producto);
    }

    public async Task<bool> ExistsByNameAsync(string nombre, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        var normalizedName = nombre.Trim().ToLower();

        return await context.Productos.AnyAsync(p =>
            p.Nombre.ToLower() == normalizedName &&
            (!excludeId.HasValue || p.Id != excludeId.Value),
            cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
