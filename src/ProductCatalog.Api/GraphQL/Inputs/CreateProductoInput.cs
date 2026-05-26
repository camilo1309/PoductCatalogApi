namespace ProductCatalog.Api.GraphQL.Inputs;

public sealed record CreateProductoInput(
    string Nombre,
    string Descripcion,
    decimal Precio
);
