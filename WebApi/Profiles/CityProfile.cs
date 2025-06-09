using AutoMapper;

namespace web_api.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.City.CityRegisterDTO>();
            CreateMap<Models.City.CityRegisterDTO, Entities.City>();
        }
    }
}
