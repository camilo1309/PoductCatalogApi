namespace ProductCatalog.Api.GraphQL.Inputs;

public sealed record UpdateProductoInput(
    int Id,
    string Nombre,
    string Descripcion,
    decimal Precio
);
