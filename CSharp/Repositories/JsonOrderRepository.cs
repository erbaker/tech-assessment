using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CSharp.Models;

namespace CSharp.Repositories;

public class JsonOrderRepository : IOrderRepository
{
    private readonly string _ordersFilePath;
    private readonly ILogger<JsonOrderRepository> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private Dictionary<string, Order>? _orders;

    public JsonOrderRepository(
        IHostEnvironment environment,
        ILogger<JsonOrderRepository> logger,
        ICustomerRepository customerRepository)
    {
        var expectedPath = Path.Combine(environment.ContentRootPath, "Data", "orders.json");
        // Validate the path is exactly what we expect
        var normalizedPath = Path.GetFullPath(expectedPath);
        if (!normalizedPath.EndsWith(Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "orders.json"))
        {
            throw new InvalidOperationException("Invalid file path");
        }
        _ordersFilePath = normalizedPath;
        _logger = logger;
        _customerRepository = customerRepository;
    }

    private async Task LoadOrdersAsync()
    {
        if (_orders != null) return;

        await _lock.WaitAsync();
        try
        {
            if (_orders != null) return; // Double-check after acquiring lock

            if (!File.Exists(_ordersFilePath))
            {
                await File.WriteAllTextAsync(_ordersFilePath, "{}");
            }

            var jsonString = await File.ReadAllTextAsync(_ordersFilePath);
            _orders = JsonSerializer.Deserialize<Dictionary<string, Order>>(jsonString) ?? new Dictionary<string, Order>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading orders from JSON file");
            _orders = new Dictionary<string, Order>();
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task SaveOrdersAsync()
    {
        if (_orders == null) return;

        await _lock.WaitAsync();
        try
        {
            var jsonString = JsonSerializer.Serialize(_orders, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_ordersFilePath, jsonString);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving orders to JSON file");
            throw;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<Order> CreateOrderAsync(OrderCreateDto orderCreate)
    {
        // Verify customer exists
        var customer = await _customerRepository.GetCustomerAsync(orderCreate.CustomerId);
        if (customer == null)
        {
            throw new InvalidOperationException($"Customer {orderCreate.CustomerId} not found");
        }

        await LoadOrdersAsync();

        var order = new Order
        {
            Id = $"ord_{Guid.NewGuid().ToString()[..8]}",
            CustomerId = orderCreate.CustomerId,
            ProductId = orderCreate.ProductId,
            Quantity = orderCreate.Quantity,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _orders![order.Id] = order;
        await SaveOrdersAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId)
    {
        await LoadOrdersAsync();

        return _orders!.Values
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt);
    }

    public async Task<Order?> GetOrderAsync(string orderId)
    {
        await LoadOrdersAsync();
        _orders!.TryGetValue(orderId, out var order);
        return order;
    }

    public async Task<Order?> UpdateOrderAsync(string orderId, OrderUpdateDto orderUpdate)
    {
        await LoadOrdersAsync();

        if (!_orders!.TryGetValue(orderId, out var order) || order.Status == OrderStatus.Cancelled)
        {
            return null;
        }

        if (orderUpdate.Quantity.HasValue)
        {
            order.Quantity = orderUpdate.Quantity.Value;
        }

        if (orderUpdate.Status.HasValue)
        {
            order.Status = orderUpdate.Status.Value;
        }

        order.UpdatedAt = DateTime.UtcNow;
        await SaveOrdersAsync();

        return order;
    }

    public async Task<Order?> CancelOrderAsync(string orderId)
    {
        await LoadOrdersAsync();

        if (!_orders!.TryGetValue(orderId, out var order) || order.Status == OrderStatus.Cancelled)
        {
            return null;
        }

        order.Status = OrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;
        await SaveOrdersAsync();

        return order;
    }
}
