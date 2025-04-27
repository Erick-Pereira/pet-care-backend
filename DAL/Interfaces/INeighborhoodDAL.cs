using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface INeighborhoodDAL : ICRUD<Neighborhood>
    {
        Task<SingleResponse<Neighborhood>> FindByNeighborhood(Neighborhood neighborhood);

        Task<SingleResponse<Neighborhood>> UpdateReturnObject(Neighborhood neighborhood);

        Task<SingleResponse<Neighborhood>> InsertReturnObject(Neighborhood neighborhood);

        Task<int> CountAllByCityId(Guid cityId);
    }
}