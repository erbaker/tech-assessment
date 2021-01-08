using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		private OrdersService _ordersService = new OrdersService();

		[HttpGet]
		[Route("{customer}")]
		public void GetOrdersByCustomer(string customer)
		{
			_ordersService.GetOrdersByCustomer(customer);
		}

		[HttpPost]
		[Route("update")]
		public void UpdateOrder([FromBody] Models.UpdateOrderModel updateOrderModel)
		{
			_ordersService.UpdateOrder(updateOrderModel.id, updateOrderModel.items);
		}

		[HttpPost]
		public Guid CreateOrder([FromBody] Models.CreateOrderModel createOrderModel)
		{
			var orderId = _ordersService.CreateOrder(createOrderModel.customer, createOrderModel.items);
			return orderId;
		}

		[HttpPost]
		[Route("cancel/{orderId}")]
		public void CancelOrder(Guid orderId)
		{
			_ordersService.CancelOrder(orderId);
		}
	}
}
