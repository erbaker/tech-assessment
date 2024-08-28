using CSharp.Accessors.DataTransferObjects;

namespace CSharp.Accessors
{
    public  interface IOrderAccessor
    {
        /// <summary>
        /// Creates a new order to be added to the current list of orders.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool CreateOrder(Order order);
    }
}
