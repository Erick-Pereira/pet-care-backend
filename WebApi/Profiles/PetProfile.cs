using AutoMapper;

namespace web_api.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Entities.Pet, Models.Pet.PetRegistrationDTO>();
            CreateMap<Models.Pet.PetRegistrationDTO, Entities.Pet>();
            //CreateMap<Models.Pet.PetRegistrationDTO, Entities.Pet>().ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.owner));
            //CreateMap<Entities.Pet, Models.Pet.PetRegistrationDTO>().ForMember(dest => dest.owner, opt => opt.MapFrom(src => src.Owner));
            //CreateMap<Models.Pet.PetRegistrationDTO, Entities.Pet>().ForMember(dest => dest.Owner, opt => opt.Ignore()).ForMember(dest => dest.Specie, opt => opt.Ignore()).ForMember(dest => dest.Breed, opt => opt.Ignore());
        }
    }
}