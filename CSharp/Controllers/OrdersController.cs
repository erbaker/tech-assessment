using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrdersController : ControllerBase
	{

		OrdersService ordersService;

        public OrdersController(OrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

		[HttpGet]
		public List<Order> GetOrders()
		{
			return ordersService.GetOrders();
		}
	}
}
