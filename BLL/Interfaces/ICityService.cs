using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface ICityService : ICRUD<City>
    {
        Task<SingleResponse<City>> InsertReturnObject(City item);

        Task<SingleResponse<City>> UpdateReturnObject(City item);

        Task<SingleResponse<City>> FindOrCreateNew(City item);

        Task<SingleResponse<City>> FindOrCreateOrSwitch(City city);
    }
}