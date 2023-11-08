using CSharp.Models;
using CSharp.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharp.UnitTest
{
    public class UnitTest
    {
        [Test]
        public void Test_Get_All_Orders()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<OrdersContext>().UseSqlite(connection).Options;

            using (var context = new OrdersContext(options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new OrdersContext(options))
            {
                List<string> itemsList = new List<string>();
                itemsList.Add("hammer");
                itemsList.Add("nails");

                List<int> quantityList = new List<int>();
                quantityList.Add(1);
                quantityList.Add(40);
                context.Orders.Add(new Orders { Id = 1, customer=new Customer(), items = itemsList, quantity = quantityList, orderTimestamp=new System.DateTime(), orderStatus = OrderStatus.InProgress });
                context.SaveChanges();
            }

            using (var context = new OrdersContext(options))
            {
                var provider = new OrderRepository(context);
                var orders = provider.GetAllOrdersAsync();

                Assert.AreEqual(1, orders.Result.Count);
            }
        }
    }
}
