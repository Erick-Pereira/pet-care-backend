using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.Interfaces;
using Entities;

namespace BLL.Impl
{
    internal class UserServiceImpl : IUserService
    {
        private readonly IUserDAL _userDAL;

        public UserServiceImpl(IUserDAL userDAL)
        {
            _userDAL = userDAL ?? throw new ArgumentNullException(nameof(userDAL));
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _userDAL.Delete(id);
        }

        public async Task<SingleResponse<User>> Get(Guid id)
        {
            return await _userDAL.Get(id);
        }

        public async Task<DataResponse<User>> Get(int skip, int take)
        {
            return await _userDAL.Get(skip, take);
        }

        public async Task<Response> Insert(User item)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse(); // Converte ValidationResult para Response
            }

            return await _userDAL.Insert(item);
        }

        public async Task<Response> Update(User item)
        {
            var validator = new UserValidator();
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse(); // Converte ValidationResult para Response
            }

            return await _userDAL.Update(item);
        }
    }
}