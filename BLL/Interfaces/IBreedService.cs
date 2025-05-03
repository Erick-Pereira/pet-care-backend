using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IBreedService : ICRUD<Breed>
    {
        Task<SingleResponse<Breed>> ToggleActive(Guid id);
    }
}