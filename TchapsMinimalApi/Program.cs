using Microsoft.EntityFrameworkCore;
using TchapsMinimalApi.Data;
using TchapsMinimalApi.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDatabase>(options =>
    options.UseSqlite("Data Source=productdatabase.db"));

builder.Services.AddScoped<ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/api/products", (ProductRepository db) =>
{

    return db.GetProducts();
})
.WithName("GetProducts")
.WithOpenApi();


app.MapGet("/api/products/{id}", (ProductRepository db, int id) =>
{
    var product = db.GetProduct(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});


app.MapPost("/api/products", (ProductRepository db, Product product) =>
{
    db.AddProduct(product);
    return Results.Created($"/api/products/{product.Id}", product);
});


app.MapPut("/api/products/{id}", (ProductRepository db, int id, Product product) =>
{
    var existingProduct = db.GetProduct(id);
    if (existingProduct is null)
    {
        return Results.NotFound();
    }
    product.Id = id;
    db.UpdateProduct(product);
    return Results.NoContent();
});

app.MapDelete("/api/products/{id}", (ProductRepository db, int id) =>
{
    var existingProduct = db.GetProduct(id);
    if (existingProduct is null)
    {
        return Results.NotFound();
    }
    db.DeleteProduct(id);
    return Results.NoContent();
});

app.Run();
