using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface IUserDAL : ICRUD<User>
    {
        Task<DataResponse<User>> Get(int skip, int take, bool? active = null);

        Task<int> CountAllByAddressId(Guid addressId);

        Task<SingleResponse<User>> GetByEmail(string email);
    }
}