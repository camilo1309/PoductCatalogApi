using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Services;
using ProductCatalog.Application.Services.Impl;
using ProductCatalog.Grpc.Services;
using ProductCatalog.Infrastructure.Persistence;
using ProductCatalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

app.MapGrpcService<ProductoGrpcService>();

app.MapGet("/", () => "Servicio gRPC de ProductCatalog activo. Use un cliente gRPC como Postman, Insomnia o grpcurl para probar los métodos CRUD.");

app.Run();
