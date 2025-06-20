using AutoMapper;

namespace web_api.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Entities.Address, Models.Address.AddressRegistrationDTO>();
            CreateMap<Models.Address.AddressRegistrationDTO, Entities.Address>();
        }
    }
}