using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using CSharp.Controllers;
using CSharp.Models;
using CSharp.Repositories;
using Moq;
using Xunit;

namespace CSharp.Tests;

public class OrdersControllerTests
{
    private readonly OrdersController _controller;
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Dictionary<string, Order> _orders;

    public OrdersControllerTests()
    {
        _orders = new Dictionary<string, Order>();
        _mockOrderRepository = new Mock<IOrderRepository>();

        // Setup CreateOrderAsync
        _mockOrderRepository.Setup(repo => repo.CreateOrderAsync(It.IsAny<OrderCreateDto>()))
            .ReturnsAsync((OrderCreateDto dto) =>
            {
                var order = new Order
                {
                    Id = $"ord_{Guid.NewGuid().ToString()[..8]}",
                    CustomerId = dto.CustomerId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };
                _orders[order.Id] = order;
                return order;
            });

        // Setup GetOrdersByCustomerAsync
        _mockOrderRepository.Setup(repo => repo.GetOrdersByCustomerAsync(It.IsAny<string>()))
            .ReturnsAsync((string customerId) =>
                _orders.Values.Where(o => o.CustomerId == customerId).OrderByDescending(o => o.CreatedAt));

        // Setup GetOrderAsync
        _mockOrderRepository.Setup(repo => repo.GetOrderAsync(It.IsAny<string>()))
            .ReturnsAsync((string orderId) => _orders.TryGetValue(orderId, out var order) ? order : null);

        // Setup UpdateOrderAsync
        _mockOrderRepository.Setup(repo => repo.UpdateOrderAsync(It.IsAny<string>(), It.IsAny<OrderUpdateDto>()))
            .ReturnsAsync((string orderId, OrderUpdateDto updateDto) =>
            {
                if (!_orders.TryGetValue(orderId, out var order) || order.Status == OrderStatus.Cancelled)
                    return null;

                if (updateDto.Quantity.HasValue)
                    order.Quantity = updateDto.Quantity.Value;
                if (updateDto.Status.HasValue)
                    order.Status = updateDto.Status.Value;

                order.UpdatedAt = DateTime.UtcNow;
                return order;
            });

        // Setup CancelOrderAsync
        _mockOrderRepository.Setup(repo => repo.CancelOrderAsync(It.IsAny<string>()))
            .ReturnsAsync((string orderId) =>
            {
                if (!_orders.TryGetValue(orderId, out var order) || order.Status == OrderStatus.Cancelled)
                    return null;

                order.Status = OrderStatus.Cancelled;
                order.UpdatedAt = DateTime.UtcNow;
                return order;
            });

        _controller = new OrdersController(_mockOrderRepository.Object);
    }

    [Fact]
    public async Task CreateOrder_ReturnsCreatedResult()
    {
        // Arrange
        var orderCreate = new OrderCreateDto
        {
            CustomerId = "cust_123",
            ProductId = "prod_456",
            Quantity = 2
        };

        // Act
        var result = await _controller.CreateOrder(orderCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var order = Assert.IsType<Order>(createdAtActionResult.Value);
        Assert.Equal(orderCreate.CustomerId, order.CustomerId);
        Assert.Equal(orderCreate.ProductId, order.ProductId);
        Assert.Equal(orderCreate.Quantity, order.Quantity);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.Matches(@"^ord_[a-zA-Z0-9]{8}$", order.Id);
    }

    [Fact]
    public async Task GetCustomerOrders_ReturnsOrders()
    {
        // Arrange
        var customerId = "cust_789";
        var orderCreate = new OrderCreateDto
        {
            CustomerId = customerId,
            ProductId = "prod_456",
            Quantity = 1
        };
        await _controller.CreateOrder(orderCreate);
        await _controller.CreateOrder(orderCreate);

        // Act
        var result = await _controller.GetCustomerOrders(customerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var orders = Assert.IsAssignableFrom<IEnumerable<Order>>(okResult.Value);
        Assert.Equal(2, orders.Count());
        Assert.All(orders, order => Assert.Equal(customerId, order.CustomerId));
    }

    [Fact]
    public async Task UpdateOrder_ReturnsUpdatedOrder()
    {
        // Arrange
        var orderCreate = new OrderCreateDto
        {
            CustomerId = "cust_123",
            ProductId = "prod_456",
            Quantity = 1
        };
        var createResult = await _controller.CreateOrder(orderCreate);
        var createdOrder = (createResult as CreatedAtActionResult)?.Value as Order;
        Assert.NotNull(createdOrder);

        var updateDto = new OrderUpdateDto
        {
            Quantity = 3,
            Status = OrderStatus.Confirmed
        };

        // Act
        var result = await _controller.UpdateOrder(createdOrder.Id, updateDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedOrder = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(updateDto.Quantity, updatedOrder.Quantity);
        Assert.Equal(updateDto.Status, updatedOrder.Status);
    }

    [Fact]
    public async Task CancelOrder_ReturnsCancelledOrder()
    {
        // Arrange
        var orderCreate = new OrderCreateDto
        {
            CustomerId = "cust_123",
            ProductId = "prod_456",
            Quantity = 1
        };
        var createResult = await _controller.CreateOrder(orderCreate);
        var createdOrder = (createResult as CreatedAtActionResult)?.Value as Order;
        Assert.NotNull(createdOrder);

        // Act
        var result = await _controller.CancelOrder(createdOrder.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var cancelledOrder = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(OrderStatus.Cancelled, cancelledOrder.Status);
    }

    [Fact]
    public async Task UpdateCancelledOrder_ReturnsNotFound()
    {
        // Arrange
        var orderCreate = new OrderCreateDto
        {
            CustomerId = "cust_123",
            ProductId = "prod_456",
            Quantity = 1
        };
        var createResult = await _controller.CreateOrder(orderCreate);
        var createdOrder = (createResult as CreatedAtActionResult)?.Value as Order;
        Assert.NotNull(createdOrder);

        await _controller.CancelOrder(createdOrder.Id);

        var updateDto = new OrderUpdateDto { Quantity = 5 };

        // Act
        var result = await _controller.UpdateOrder(createdOrder.Id, updateDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}