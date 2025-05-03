using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IUserService : ICRUD<User>
    {
        Task<SingleResponse<User>> GetByEmail(string email);
    }
}