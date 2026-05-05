namespace ProductCatalog.Application.DTOs;

public sealed record ProductoDto(
    int Id,
    string Nombre,
    string Descripcion,
    decimal Precio,
    DateTime FechaCreacion,
    DateTime? FechaActualizacion
);
