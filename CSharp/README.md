# Orders REST API - C# Implementation

A modern REST API for managing orders built with ASP.NET Core 8.0.

## Features

- Create new orders
- List all orders by customer
- Update existing orders
- Cancel orders
- OpenAPI/Swagger documentation
- Comprehensive test suite
- Input validation
- Async/await pattern
- Json file backed repository pattern

## Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

## Setup

1. Clone the repository
2. Navigate to the CSharp directory
3. Restore dependencies:
```bash
dotnet restore
```

## Running the API

Start the development server:
```bash
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5004
- HTTPS: https://localhost:5005

API documentation is available at:
- Swagger UI: https://localhost:5005/swagger

## Running Tests

```bash
dotnet test
```

## API Endpoints

### Create Order
- **POST** `/api/orders`
- Request body:
```json
{
    "customerId": "1",
    "productId": "prod_456",
    "quantity": 2
}
```

### List Customer Orders
- **GET** `/api/customers/{customerId}/orders`

### Update Order
- **PATCH** `/api/orders/{orderId}`
- Request body:
```json
{
    "quantity": 3,
    "status": "Confirmed"
}
```

### Cancel Order
- **POST** `/api/orders/{orderId}/cancel`

## Implementation Details

- Uses ASP.NET Core 8.0 Web API
- Implements Repository pattern for data access
- Uses dependency injection for loose coupling
- Includes comprehensive input validation
- Provides detailed API documentation via Swagger/OpenAPI
- Includes unit tests using xUnit
- Uses async/await pattern for scalability 