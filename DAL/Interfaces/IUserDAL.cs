using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface IUserDAL : ICRUD<User>
    {
        Task<DataResponse<User>> Get(int skip, int take, string? createdBy = null, bool? active = null);
    }
}