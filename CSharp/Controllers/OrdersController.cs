using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Services
{
	[ApiController]
	[Route("[controller]")]
	public class OrdersController : ControllerBase
	{

		private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

		[HttpGet]
		public List<Order> GetOrders()
		{
			return ordersService.GetOrders();
		}

        public Order CreateOrder()
        {
			return ordersService.CreateOrder();
        }
    }
}
