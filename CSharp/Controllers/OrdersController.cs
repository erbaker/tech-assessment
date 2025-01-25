using Microsoft.AspNetCore.Mvc;
using CSharp.Models;
using CSharp.Repositories;

namespace CSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Creates a new order
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(OrderCreateDto orderCreate)
    {
        var order = await _orderRepository.CreateOrderAsync(orderCreate);
        return CreatedAtAction(nameof(GetOrder), new { orderId = order.Id }, order);
    }

    /// <summary>
    /// Gets all orders for a specific customer
    /// </summary>
    [HttpGet("~/api/customers/{customerId}/orders")]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerOrders(string customerId)
    {
        var orders = await _orderRepository.GetOrdersByCustomerAsync(customerId);
        return Ok(orders);
    }

    /// <summary>
    /// Gets a specific order by ID
    /// </summary>
    [HttpGet("{orderId}")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(string orderId)
    {
        var order = await _orderRepository.GetOrderAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    /// <summary>
    /// Updates an existing order
    /// </summary>
    [HttpPatch("{orderId}")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrder(string orderId, OrderUpdateDto orderUpdate)
    {
        var updatedOrder = await _orderRepository.UpdateOrderAsync(orderId, orderUpdate);
        if (updatedOrder == null)
        {
            return NotFound("Order not found or cannot be updated");
        }
        return Ok(updatedOrder);
    }

    /// <summary>
    /// Cancels an existing order
    /// </summary>
    [HttpPost("{orderId}/cancel")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelOrder(string orderId)
    {
        var cancelledOrder = await _orderRepository.CancelOrderAsync(orderId);
        if (cancelledOrder == null)
        {
            return NotFound("Order not found or already cancelled");
        }
        return Ok(cancelledOrder);
    }
}