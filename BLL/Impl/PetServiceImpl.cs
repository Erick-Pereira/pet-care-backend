using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class PetServiceImpl : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PetValidator petValidator;
        private readonly UserValidator userValidator;

        public PetServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            petValidator = new PetValidator();
            userValidator = new UserValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.PetRepository.Delete(id);
        }

        public async Task<SingleResponse<Pet>> Get(Guid id)
        {
            return await _unitOfWork.PetRepository.Get(id);
        }

        public async Task<DataResponse<Pet>> Get(int skip, int take)
        {
            return await _unitOfWork.PetRepository.Get(skip, take);
        }

        public async Task<Response> Insert(Pet item)
        {
            var validationResult = petValidator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.PetRepository.Insert(item);
        }

        public async Task<Response> Update(Pet item)
        {
            var validationResult = petValidator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.PetRepository.Update(item);
        }

        public async Task<Response> RegisterPetWithOwner(Pet request)
        {
            var petValidationResult = petValidator.Validate(request);
            if (!petValidationResult.IsValid)
            {
                return petValidationResult.ToResponse();
            }

            var userValidationResult = userValidator.Validate(request.Owner);
            if (!petValidationResult.IsValid)
            {
                return petValidationResult.ToResponse();
            }

            // Insert owner into the database
            var ownerResponse = await _unitOfWork.UserRepository.Insert(request.Owner);
            if ((bool)!ownerResponse.Success)
            {
                return ownerResponse;
            }

            request.OwnerId = request.Owner.Id;

            // Insert pet into the database
            var petResponse = await _unitOfWork.PetRepository.Insert(request);
            return (bool)!petResponse.Success ? petResponse : ResponseFactory.CreateInstance().CreateSuccessResponse("Pet and owner registered successfully.");
        }

        public async Task<SingleResponse<Pet>> ToggleActive(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.PetRepository.Get(id);
                if (!entity.Success == true || entity.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Pet>("Pet not found");

                entity.Item.Active = !entity.Item.Active;
                var updateResponse = await _unitOfWork.PetRepository.Update(entity.Item);

                if (!updateResponse.Success == true)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Pet>(updateResponse.Message);

                return ResponseFactory.CreateSuccessSingleResponse(entity.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Pet>("Error toggling pet status", ex);
            }
        }
    }
}