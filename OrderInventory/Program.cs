using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderInventory.Data;
using OrderInventory.Services.Inventories;
using OrderInventory.Services.Orders;
using OrderInventory.Validations;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=orders.db"));

// Services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

// Controllers
builder.Services.AddControllers();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<OrderRequestValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure DB created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderInventory API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting(); 
app.MapControllers();
app.Run();
