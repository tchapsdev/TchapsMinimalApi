# TchapsMinimalApi

TchapsMinimalApi is a sample ASP.NET Core Minimal API that demonstrates product
CRUD operations. It uses Entity Framework Core with a local SQLite database and
exposes an OpenAPI document and Swagger UI in the Development environment.

## Requirements

- .NET 8 SDK
- Entity Framework Core CLI (`dotnet-ef`) to create the local database from the
  included migrations

## Run locally

From the repository root, restore dependencies and apply the database
migrations:

```bash
dotnet restore
dotnet ef database update --project TchapsMinimalApi
dotnet run --project TchapsMinimalApi
```

The application uses `productdatabase.db` in its working directory. In
Development, Swagger UI is available at `/swagger` on the application URL
printed by `dotnet run`.

## API

All endpoints are under `/api/products`:

| Method | Route | Description | Success response |
| --- | --- | --- | --- |
| `GET` | `/api/products` | List all products | `200 OK` with a product array |
| `GET` | `/api/products/{id}` | Get a product by ID | `200 OK` with a product, or `404 Not Found` |
| `POST` | `/api/products` | Create a product | `201 Created` with the created product |
| `PUT` | `/api/products/{id}` | Replace a product | `204 No Content`, or `404 Not Found` |
| `DELETE` | `/api/products/{id}` | Delete a product | `204 No Content`, or `404 Not Found` |

Example product JSON:

```json
{
  "name": "Desk lamp",
  "price": 29.99,
  "description": "Adjustable LED desk lamp",
  "category": "Home",
  "imageUrl": "https://example.com/desk-lamp.jpg",
  "isAvailable": true
}
```

The `id` is assigned by the database when a product is created. Product names
are required and limited to 100 characters. Descriptions, categories, and image
URLs are limited to 500, 50, and 200 characters respectively.

## Project structure

- `TchapsMinimalApi/Program.cs` registers the database and repository and maps
  the HTTP endpoints.
- `TchapsMinimalApi/Model/Product.cs` defines the product data model.
- `TchapsMinimalApi/Data/ProductDatabase.cs` configures the SQLite-backed
  Entity Framework Core context.
- `TchapsMinimalApi/Data/ProductRepository.cs` contains product data operations.
- `TchapsMinimalApi/Migrations/` contains the database schema migrations.
