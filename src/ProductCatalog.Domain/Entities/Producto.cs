namespace ProductCatalog.Domain.Entities;

public class Producto
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public decimal Precio { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime? FechaActualizacion { get; private set; }

    private Producto() { }

    public Producto(string nombre, string descripcion, decimal precio)
    {
        SetNombre(nombre);
        SetDescripcion(descripcion);
        SetPrecio(precio);
        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(string nombre, string descripcion, decimal precio)
    {
        SetNombre(nombre);
        SetDescripcion(descripcion);
        SetPrecio(precio);
        FechaActualizacion = DateTime.UtcNow;
    }

    private void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del producto es obligatorio.", nameof(nombre));

        if (nombre.Length > 100)
            throw new ArgumentException("El nombre del producto no puede superar los 100 caracteres.", nameof(nombre));

        Nombre = nombre.Trim();
    }

    private void SetDescripcion(string descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("La descripción del producto es obligatoria.", nameof(descripcion));

        if (descripcion.Length > 300)
            throw new ArgumentException("La descripción del producto no puede superar los 300 caracteres.", nameof(descripcion));

        Descripcion = descripcion.Trim();
    }

    private void SetPrecio(decimal precio)
    {
        if (precio <= 0)
            throw new ArgumentException("El precio debe ser mayor a cero.", nameof(precio));

        Precio = precio;
    }
}
