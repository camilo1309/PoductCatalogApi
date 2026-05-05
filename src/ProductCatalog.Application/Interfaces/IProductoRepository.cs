using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Interfaces;

public interface IProductoRepository
{
    Task<IReadOnlyList<Producto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Producto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Producto producto, CancellationToken cancellationToken = default);
    void Update(Producto producto);
    void Delete(Producto producto);
    Task<bool> ExistsByNameAsync(string nombre, int? excludeId = null, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
