using AutoMapper;
using CSharp.Managers.ViewModels;

namespace CSharp.Managers.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, Accessors.DataTransferObjects.Order>()
                .ForMember(
                    memberOption => memberOption.OrderId,
                    destinationMember => destinationMember.Ignore())
                .ForMember(
                    memberOption => memberOption.IsActive,
                    destinationMember => destinationMember.Ignore())
                .ReverseMap();
        }
    }
}
