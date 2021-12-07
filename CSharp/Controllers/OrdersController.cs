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
	public class OrdersController : ControllerBase
	{ 
		public OrdersController()
        {

        }

		[HttpGet]
		public List<Order> GetOrders()
		{
			var orders = new List<Order>() { new Order() };
			return orders;
		}
	}
}
