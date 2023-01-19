using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{ 
		// to scale this would have to be made into a DB layer in of itself instead of in memory 
        private static List<Order> _ordersStatic = new List<Order>();
		
        [HttpPost]
        public ActionResult Post([FromBody] Order newOrder)
        {
            var maxOrderId = _ordersStatic.Max(o => o.Id);
            
            newOrder.Id = maxOrderId + 1;
            newOrder.Status = OrderStatusEnum.Received;
            newOrder.ReceivedDateTime = DateTime.Now;
            newOrder.LastUpdatedDateTime = DateTime.Now;

            _ordersStatic.Add(newOrder);

            return Created($"api/orders/{newOrder.Id}", newOrder);
        }

        // GET api/orders?customerId=1
        [HttpGet]
        public ActionResult<List<Order>> GetOrdersByCustomerId([FromQuery] int customerId)
        {
            var customerOrders = _ordersStatic.Where(order => order.CustomerId == customerId).ToList();

            return Ok(customerOrders);
        }

        // PUT api/orders/5
        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            if (!_ordersStatic.Any(order => order.Id == id))
            {
                return NotFound();
            }

            _ordersStatic.Where(order => order.Id == id).Select(order => { order.Status = updatedOrder.Status; return order; });

            return NoContent();
        }

        // PUT api/orders/1/cancel
        [HttpPut("{id}/cancel")]
        public ActionResult CancelOrder(int id)
        {
            if (!_ordersStatic.Any(order => order.Id == id))
            {
                return NotFound();
            }

            _ordersStatic.Where(order => order.Id == id).Select(order => { order.Status = OrderStatusEnum.Canceled; return order; });

            return NoContent();
        }
	}
}
