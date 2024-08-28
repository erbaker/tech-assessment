using AutoMapper;
using CSharp.Managers.ViewModels;

namespace CSharp.Managers.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, Accessors.DataTransferObjects.Order>()
                .ReverseMap();
        }
    }
}
