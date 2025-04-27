using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IAddressService : ICRUD<Address>
    {
        Task<SingleResponse<Address>> InsertReturnObject(Address item);

        Task<SingleResponse<Address>> UpdateReturnObject(Address item);

        Task<SingleResponse<Address>> FindOrCreateNew(Address item);

        Task<SingleResponse<Address>> FindOrCreateOrSwitch(Address address);
    }
}