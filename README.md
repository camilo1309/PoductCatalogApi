# ProductCatalogApi

Backend RESTful CRUD para la entidad Producto, desarrollado en ASP.NET Core Web API con Clean Architecture, SOLID, Entity Framework Core, migraciones y Swagger.

## Estructura

```txt
src/
├── ProductCatalog.Domain
├── ProductCatalog.Application
├── ProductCatalog.Infrastructure
└── ProductCatalog.Api
```

## Requisitos

- .NET 8 SDK
- SQL Server LocalDB o SQL Server Express
- Visual Studio 2022 / VS Code

## Configuración de base de datos

La cadena de conexión está en:

```txt
src/ProductCatalog.Api/appsettings.json
```

Por defecto usa SQL Server LocalDB:

```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductCatalogDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

## Crear migración

```bash
dotnet ef migrations add InitialCreate --project src/ProductCatalog.Infrastructure --startup-project src/ProductCatalog.Api
```

## Crear base de datos

```bash
dotnet ef database update --project src/ProductCatalog.Infrastructure --startup-project src/ProductCatalog.Api
```

## Ejecutar API

```bash
dotnet run --project src/ProductCatalog.Api
```

Abrir Swagger:

```txt
https://localhost:5001/swagger
```

o revisar la URL que muestre la consola.

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| GET | /api/productos | Lista todos los productos |
| GET | /api/productos/{id} | Consulta un producto por id |
| POST | /api/productos | Crea un producto |
| PUT | /api/productos/{id} | Actualiza un producto |
| DELETE | /api/productos/{id} | Elimina un producto |

## JSON para POST

```json
{
  "nombre": "Teclado mecánico",
  "descripcion": "Teclado RGB para programación",
  "precio": 180000
}
```

## JSON para PUT

```json
{
  "nombre": "Teclado mecánico actualizado",
  "descripcion": "Teclado RGB con switches azules",
  "precio": 200000
}
```
