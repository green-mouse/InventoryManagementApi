# Inventory Management API

Simple ASP.NET Core Web API project for inventory and category management.

## Features

- Product CRUD operations
- Category CRUD operations
- Product search endpoint
- Entity Framework Core with MySQL
- DTO pattern
- Input validation
- Entity relationships
- Swagger/OpenAPI documentation
- Async/await API methods

## Technologies

- ASP.NET Core
- C#
- Entity Framework Core
- MySQL
- Swagger

## Endpoints

### Products

- GET /api/products
- GET /api/products/{id}
- GET /api/products/search?name=value
- POST /api/products
- PUT /api/products/{id}
- DELETE /api/products/{id}

### Categories

- GET /api/categories
- GET /api/categories/{id}
- POST /api/categories
- DELETE /api/categories/{id}

## Running the project

1. Configure MySQL connection string in `appsettings.json`
2. Run migrations:

```bash
dotnet ef database update
```

3. Run the application:

```bash
dotnet run
```

## Author

Boris Murković
