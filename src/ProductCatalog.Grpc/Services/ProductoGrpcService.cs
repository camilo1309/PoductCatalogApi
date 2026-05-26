using Grpc.Core;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Services;
using ProductCatalog.Application.Services.Impl;

namespace ProductCatalog.Grpc.Services;

public sealed class ProductoGrpcService(IProductoService productoService, ILogger<ProductoGrpcService> logger)
    : ProductoGrpc.ProductoGrpcBase
{
    public override async Task<ProductoListResponse> GetAll(GetAllProductosRequest request, ServerCallContext context)
    {
        var productos = await productoService.GetAllAsync(context.CancellationToken);
        var response = new ProductoListResponse();
        response.Productos.AddRange(productos.Select(ToGrpcResponse));
        return response;
    }

    public override async Task<ProductoResponse> GetById(GetProductoByIdRequest request, ServerCallContext context)
    {
        try
        {
            var producto = await productoService.GetByIdAsync(request.Id, context.CancellationToken);
            return ToGrpcResponse(producto);
        }
        catch (NotFoundException ex)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
    }

    public override async Task<ProductoResponse> Create(CreateProductoGrpcRequest request, ServerCallContext context)
    {
        try
        {
            var producto = await productoService.CreateAsync(new CreateProductoRequest
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Precio = Convert.ToDecimal(request.Precio)
            }, context.CancellationToken);

            return ToGrpcResponse(producto);
        }
        catch (BusinessException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }

    public override async Task<ProductoResponse> Update(UpdateProductoGrpcRequest request, ServerCallContext context)
    {
        try
        {
            var producto = await productoService.UpdateAsync(request.Id, new UpdateProductoRequest
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Precio = Convert.ToDecimal(request.Precio)
            }, context.CancellationToken);

            return ToGrpcResponse(producto);
        }
        catch (NotFoundException ex)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (BusinessException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
    }

    public override async Task<DeleteProductoGrpcResponse> Delete(DeleteProductoGrpcRequest request, ServerCallContext context)
    {
        try
        {
            await productoService.DeleteAsync(request.Id, context.CancellationToken);
            return new DeleteProductoGrpcResponse
            {
                Success = true,
                Message = $"Producto con id {request.Id} eliminado correctamente."
            };
        }
        catch (NotFoundException ex)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error eliminando el producto {ProductId} desde gRPC.", request.Id);
            throw new RpcException(new Status(StatusCode.Internal, "Ocurrió un error inesperado al eliminar el producto."));
        }
    }

    private static ProductoResponse ToGrpcResponse(ProductoDto producto)
    {
        return new ProductoResponse
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = Convert.ToDouble(producto.Precio),
            FechaCreacion = producto.FechaCreacion.ToString("O"),
            FechaActualizacion = producto.FechaActualizacion?.ToString("O") ?? string.Empty
        };
    }
}
