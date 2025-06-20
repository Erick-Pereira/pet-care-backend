using AutoMapper;

namespace web_api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, Models.User.UserRegistrationDTO>();
            CreateMap<Models.User.UserRegistrationDTO, Entities.User>();
        }
    }
}