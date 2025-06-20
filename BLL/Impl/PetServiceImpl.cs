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
        private readonly IUserService _userService;
        private readonly IBreedService _breedService;
        private readonly ISpecieService _specieService;
        private readonly PetValidator petValidator;
        private readonly UserValidator userValidator;

        public PetServiceImpl(IUnitOfWork unitOfWork, IUserService userService, IBreedService breedService, ISpecieService specieService)
        {
            _breedService = breedService;
            _specieService = specieService;
            _userService = userService;
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

        public async Task<DataResponse<Pet>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.PetRepository.Get(skip, take, filter);
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
            //if (request.Breed == null && (request.BreedId != default || request.BreedId != Guid.Empty))
            //{
            //    var breedResponse = await _breedService.Get(request.BreedId);
            //    if (breedResponse.Success.HasValue || breedResponse.Success.Value)
            //    {
            //        request.Breed = breedResponse.Item;
            //    }
            //}

            //if (request.Specie == null && (request.SpecieId != default || request.SpecieId != Guid.Empty))
            //{
            //    var specieResponse = await _specieService.Get(request.SpecieId);
            //    if (specieResponse.Success.HasValue || specieResponse.Success.Value)
            //    {
            //        request.Specie = specieResponse.Item;
            //    }
            //}

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

            var ownerResponse = await _userService.Insert(request.Owner);
            if (!ownerResponse.Success.HasValue || !ownerResponse.Success.Value)
            {
                return ownerResponse;
            }

            if (request.Owner.Id == null)
            {
                return ResponseFactory.CreateFailedResponse("Owner ID is null.");
            }
            request.OwnerId = request.Owner.Id;

            var petResponse = await _unitOfWork.PetRepository.Insert(request);
            if (!petResponse.Success.HasValue || !petResponse.Success.Value)
            {
                return petResponse;
            }
            return ResponseFactory.CreateInstance().CreateSuccessResponse("Pet and owner registered successfully.");
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
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Pet>(updateResponse.Message ?? "Failed to update pet status.");

                return ResponseFactory.CreateSuccessSingleResponse(entity.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Pet>("Error toggling pet status", ex);
            }
        }

        public async Task<Response> UpdateProfilePhoto(Guid petId, byte[] photoData)
        {
            var petResponse = await _unitOfWork.PetRepository.Get(petId);
            if (!petResponse.Success.Value || petResponse.Item == null)
                return ResponseFactory.CreateFailedResponse("Pet not found");

            petResponse.Item.ProfilePhoto = photoData;
            return await _unitOfWork.PetRepository.Update(petResponse.Item);
        }

        public async Task<Response> DeleteProfilePhoto(Guid petId)
        {
            var petResponse = await _unitOfWork.PetRepository.Get(petId);
            if (!petResponse.Success.Value || petResponse.Item == null)
                return ResponseFactory.CreateFailedResponse("Pet not found");

            petResponse.Item.ProfilePhoto = null;
            return await _unitOfWork.PetRepository.Update(petResponse.Item);
        }
    }
}