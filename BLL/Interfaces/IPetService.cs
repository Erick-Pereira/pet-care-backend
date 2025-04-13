using Commons.Interfaces;
using Commons.Responses;
using Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPetService : ICRUD<Pet>
    {
        Task<Response> RegisterPetWithOwner(PetRegistrationRequest request);
    }
}