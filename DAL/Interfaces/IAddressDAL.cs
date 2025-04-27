using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface IAddressDAL : ICRUD<Address>
    {
        Task<SingleResponse<Address>> FindByAddress(Address address);

        Task<SingleResponse<Address>> UpdateReturnObject(Address address);

        Task<SingleResponse<Address>> InsertReturnObject(Address address);

        Task<int> CountAllByNeighborhoodId(Guid neighborhoodId);
    }
}