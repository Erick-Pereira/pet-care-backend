using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface ICityDAL : ICRUD<City>
    {
        Task<SingleResponse<City>> InsertReturnObject(City city);

        Task<SingleResponse<City>> UpdateReturnObject(City city);

        Task<SingleResponse<City>> FindByName(City city);
    }
}