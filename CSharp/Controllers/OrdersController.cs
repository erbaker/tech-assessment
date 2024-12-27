using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OrdersApi.Services;
using OrdersApi.Models;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order newOrder)
        {
            var createdOrder = _orderService.CreateOrder(newOrder);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<IEnumerable<Order>> GetOrdersByCustomer(int customerId)
        {
            var orders = _orderService.GetOrdersByCustomer(customerId);
            if (orders == null || orders.Count == 0)
                return NotFound(new { message = "No orders found for this customer." });

            return Ok(orders);
        }

        [HttpPut("{id}")]
        public ActionResult<Order> UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            var order = _orderService.UpdateOrder(id, updatedOrder);
            if (order == null)
                return NotFound(new { message = "Order not found." });

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public ActionResult CancelOrder(int id)
        {
            var result = _orderService.CancelOrder(id);
            if (!result)
                return NotFound(new { message = "Order not found." });

            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
                return NotFound(new { message = "Order not found." });

            return Ok(order);
        }
    }
}
