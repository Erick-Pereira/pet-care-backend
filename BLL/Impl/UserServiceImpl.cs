using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Interfaces;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class UserServiceImpl : ICRUD<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;
        private readonly IHashService _hashService;
        private readonly UserValidator validator;

        public UserServiceImpl(IUnitOfWork unitOfWork, IAddressService addressService, IHashService hashService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
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

        public async Task<DataResponse<User>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.UserRepository.Get(skip, take, filter);
        }

        public async Task<Response> Insert(User item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();

            item.PhoneNumber = item.PhoneNumber.StringCleaner();
            item.Password = await _hashService.HashPasswordAsync(item.Password);

            var addressResponse = await _addressService.FindOrCreateNew(item.Address);
            if (addressResponse.Success == true && addressResponse.Item != null)
            {
                if (addressResponse.Item.Id != null)
                {
                    item.AddressId = addressResponse.Item.Id;
                }
                item.Address = addressResponse.Item;
            }

            return await _unitOfWork.UserRepository.Insert(item);
        }

        public async Task<Response> Update(User item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();

            // Hash da senha antes de atualizar
            item.Password = await _hashService.HashPasswordAsync(item.Password);

            var addressResponse = await _addressService.FindOrCreateOrSwitch(item.Address);
            if (addressResponse.Success.GetValueOrDefault() && addressResponse.Item != null && addressResponse.Item.Id != null)
            {
                item.AddressId = addressResponse.Item.Id;
                item.Address = addressResponse.Item;
            }

            return await _unitOfWork.UserRepository.Update(item);
        }

        public async Task<SingleResponse<User>> GetByEmail(string email)
        {
            return await _unitOfWork.UserRepository.GetByEmail(email);
        }

        public async Task<Response> UpdateProfilePhoto(Guid userId, byte[] photoData)
        {
            var userResponse = await _unitOfWork.UserRepository.Get(userId);
            if (!userResponse.Success.Value || userResponse.Item == null)
                return ResponseFactory.CreateFailedResponse("User not found");

            userResponse.Item.ProfilePhoto = photoData;
            return await _unitOfWork.UserRepository.Update(userResponse.Item);
        }

        public async Task<Response> DeleteProfilePhoto(Guid userId)
        {
            var userResponse = await _unitOfWork.UserRepository.Get(userId);
            if (!userResponse.Success.Value || userResponse.Item == null)
                return ResponseFactory.CreateFailedResponse("User not found");

            userResponse.Item.ProfilePhoto = null;
            return await _unitOfWork.UserRepository.Update(userResponse.Item);
        }
    }
}