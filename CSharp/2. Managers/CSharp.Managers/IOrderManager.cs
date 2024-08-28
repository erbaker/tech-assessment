using CSharp.Managers.ViewModels;

namespace CSharp.Managers
{
    public interface IOrderManager
    {
        /// <summary>
        /// Maps the incoming <see cref="Order"/> to a Data transfer object to create a new order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool CreateOrderAsync(Order order);

        /// <summary>
        /// Returns a list of Order view models associated with the requested customer id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<Order> GetOrders(int customerId);
    }
}
