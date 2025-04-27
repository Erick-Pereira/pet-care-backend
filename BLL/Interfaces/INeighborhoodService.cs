using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface INeighborhoodService : ICRUD<Neighborhood>
    {
        Task<SingleResponse<Neighborhood>> InsertReturnObject(Neighborhood item);

        Task<SingleResponse<Neighborhood>> UpdateReturnObject(Neighborhood item);

        Task<SingleResponse<Neighborhood>> FindOrCreateNew(Neighborhood item);

        Task<SingleResponse<Neighborhood>> FindOrCreateOrSwitch(Neighborhood neighborhood);
    }
}