using AutoMapper;

namespace CSharp.Managers.Mapping
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<ViewModels.Address, Accessors.DataTransferObjects.Address>()
                .ReverseMap();
        }
    }
}
