using AutoMapper;

namespace web_api.Profiles
{
    public class NeighborhoodProfile : Profile
    {
        public NeighborhoodProfile()
        {
            CreateMap<Entities.Neighborhood, Models.Neighborhood.NeighborhoodRegisterDTO>();
            CreateMap<Models.Neighborhood.NeighborhoodRegisterDTO, Entities.Neighborhood>();
        }
    }
}

