using AutoMapper;

namespace web_api.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Entities.Pet, Models.Pet.PetRegistrationDTO>();
        }
    }

    {
    }
}