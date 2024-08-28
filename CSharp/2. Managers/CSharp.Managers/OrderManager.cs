using CSharp.Accessors;

namespace CSharp.Managers
{
    public class OrderManager(IOrderAccessor orderAccessor) : IOrderManager
    {
        public async Task<bool> CreateOrderAsync()
        {
            return await orderAccessor
                .CreateOrderAsync();
        }
    }
}
