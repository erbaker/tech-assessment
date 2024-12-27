using CSharp.Models;
using System.Collections.Generic;
using CSharp.Models;

namespace CSharp.Data
{
    public interface IOrderDataStore
    {
        Order Add(Order order);
        Order GetById(int id);
        List<Order> GetOrdersByCustomer(int customerId);
        Order Update(Order order);
        bool Delete(int id);
    }
}
