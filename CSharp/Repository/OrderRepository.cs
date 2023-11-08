using CSharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Task<List<Orders>> GetAllOrdersAsync();
    }
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private IOrdersContext context;

        public OrderRepository(IOrdersContext context)
        {
            this.context = context;
        }

        public async Task<List<Orders>> GetAllOrdersAsync()
        {
            List<Orders> orders = new List<Orders>();
            orders = await context.Orders.ToListAsync();
            return orders;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
