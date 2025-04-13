using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    internal class UserServiceImpl : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserValidator validator;

        public UserServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new UserValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.UserRepository.Delete(id);
        }

        public async Task<SingleResponse<User>> Get(Guid id)
        {
            return await _unitOfWork.UserRepository.Get(id);
        }

        public async Task<DataResponse<User>> Get(int skip, int take)
        {
            return await _unitOfWork.UserRepository.Get(skip, take);
        }

        public async Task<Response> Insert(User item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.UserRepository.Insert(item);
        }

        public async Task<Response> Update(User item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.UserRepository.Update(item);
        }
    }
}