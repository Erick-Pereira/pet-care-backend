using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IPetService : ICRUD<Pet>
    {
        Task<Response> RegisterPetWithOwner(PetRegistrationRequest request);
    }
}