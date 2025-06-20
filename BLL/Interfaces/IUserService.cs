using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IUserService : ICRUD<User>
    {
        Task<SingleResponse<User>> GetByEmail(string email);

        Task<Response> UpdateProfilePhoto(Guid userId, byte[] photoData);

        Task<Response> DeleteProfilePhoto(Guid userId);
    }
}