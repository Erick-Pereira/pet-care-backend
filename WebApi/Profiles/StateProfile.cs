using AutoMapper;

namespace web_api.Profiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<Entities.State, Models.State.StateRegisterDTO>();
            CreateMap<Models.State.StateRegisterDTO, Entities.State>();
        }
    }
}
