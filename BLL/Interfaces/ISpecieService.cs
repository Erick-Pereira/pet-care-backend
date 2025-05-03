using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface ISpecieService : ICRUD<Specie>
    {
        Task<SingleResponse<Specie>> ToggleActive(Guid id);
    }
}