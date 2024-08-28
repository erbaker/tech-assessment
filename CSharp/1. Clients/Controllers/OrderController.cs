using CSharp.Managers;
using CSharp.Managers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController(IOrderManager orderManager) : Controller
    {
        /// <summary>
        /// Creates a new order for the associated customer.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateOrderAsync(Order order)
        {
            var result = orderManager
                .CreateOrderAsync(order);

            return Ok(new
            {
                success = result
            });
        }

        /// <summary>
        /// Returns all orders for the requested customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetOrdersAsync(int? customerId)
        {
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateOrderAsync()
        {
            return Ok();
        }

        [HttpPut]
        [Route("Cancel")]
        public async Task<IActionResult> CancelOrderAsync(int orderId)
        {
            return Ok();
        }
    }
}
