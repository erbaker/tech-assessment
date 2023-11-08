using CSharp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace CSharp.Repository
{
    public interface IOrdersContext
    {
        public DbSet<Orders> Orders { get; set; }

        public void Dispose();
    }
    public class OrdersContext : DbContext, IOrdersContext
    {

        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options) 
        {
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        protected OrdersContext()
        {
        }

        public override void Dispose()
        {
            Database.CloseConnection();
            base.Dispose();
        }
        public virtual DbSet<Orders> Orders { get; set; }
    }
}
