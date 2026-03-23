# OrderInventory API

A small ASP.NET Core Web API for order placement and stock management.

## Features

-   Atomic order placement
-   Inventory stock management
-   Input validation using FluentValidation
-   EF Core with SQLite database
-   Structured logging
-   Swagger UI for testing endpoints
-   Unit tests for Orders and Inventory

## Tech Stack

-   ASP.NET Core (.NET 9)
-   Entity Framework Core (SQLite)
-   FluentValidation
-   Serilog
-   Swashbuckle (Swagger)
-   xUnit v3

------------------------------------------------------------------------

## API Endpoints

### POST /orders

Creates an order after validating stock availability.

**Request Body**

``` json
{
  "sku": "ITEM001",
  "quantity": 2
}
```

**Behavior** - Validates request - Checks stock - Decrements stock -
Creates order atomically

------------------------------------------------------------------------

### POST /inventory

Sets stock level for a SKU.

**Request Body**

``` json
{
  "sku": "ITEM001",
  "quantity": 10
}
```

------------------------------------------------------------------------

## Project Structure

    OrderInventorySolution.sln
    │
    ├── OrderInventory (Web API)
    │   ├── Controllers
    │   │   ├── OrdersController.cs
    │   │   └── InventoryController.cs
    │   ├── Data
    │   │   └── AppDbContext.cs
    │   ├── DTOs
    │   │   ├── OrderRequest.cs
    │   │   └── InventoryRequest.cs
    │   ├── Models
    │   │   ├── Order.cs
    │   │   └── Inventory.cs
    │   ├── Services
    │   │   ├── Orders
    │   │   ├── ├── IOrderService.cs
    │   │   │   └── OrderService.cs
    │   │   └── Inventories
    │   │       ├── IInventoryService.cs
    │   │       └── InventoryService.cs
    │   ├── Validations
    │   │   ├── OrderRequestValidator.cs
    │   │   └── InventoryRequestValidator.cs
    │   └── Program.cs
    │
    └── OrderInventory.Tests
        ├── OrdersTests.cs
        └── InventoryTests.cs

------------------------------------------------------------------------

## Running the Application

### Using Visual Studio

1.  Open solution
2.  Set OrderInventory as startup project
3.  Press Run

------------------------------------------------------------------------

## Database

SQLite database file is created automatically:

orders.db

Created on first run via EF Core.

------------------------------------------------------------------------

## Running Tests

Visual Studio: - Test → Run All Tests

or CLI:

dotnet test

------------------------------------------------------------------------

## Atomic Transaction Behavior

Order creation runs inside a database transaction:

-   Check stock
-   Deduct stock
-   Create order
-   Commit or rollback

Ensures consistency under concurrent requests.

------------------------------------------------------------------------

## Validation

Requests are validated using FluentValidation:

-   SKU required
-   Quantity must be \> 0
-   Inventory cannot be negative

Invalid requests return HTTP 400.

------------------------------------------------------------------------

## Logging

Structured logging via Serilog.

Logs include: - Order creation - Stock updates - Validation failures

