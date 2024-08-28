using AutoMapper;
using CSharp.Accessors;
using CSharp.Managers.ViewModels;

namespace CSharp.Managers
{
    public class OrderManager(IMapper mapper, IOrderAccessor orderAccessor) : IOrderManager
    {
        public bool CreateOrderAsync(Order order)
        {
            var orderDataTransferObject = mapper
                .Map<Accessors.DataTransferObjects.Order>(order);

            return orderAccessor
                .CreateOrder(orderDataTransferObject);
        }
    }
}
