using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class PetPhotoServiceImpl : IPetPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PetPhotoValidator _validator;
        private readonly FileValidator fileValidator;

        public PetPhotoServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _validator = new PetPhotoValidator();
            fileValidator = new FileValidator(true);
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.PetPhotoRepository.Delete(id);
        }

        public async Task<SingleResponse<PetPhoto>> Get(Guid id)
        {
            return await _unitOfWork.PetPhotoRepository.Get(id);
        }

        public async Task<DataResponse<PetPhoto>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.PetPhotoRepository.Get(skip, take, filter);
        }

        public async Task<DataResponse<PetPhoto>> GetByPetId(Guid petId)
        {
            return await _unitOfWork.PetPhotoRepository.GetByPetId(petId);
        }

        public async Task<Response> Insert(PetPhoto item)
        {
            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();

            return await _unitOfWork.PetPhotoRepository.Insert(item);
        }

        public async Task<Response> Update(PetPhoto item)
        {
            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();

            return await _unitOfWork.PetPhotoRepository.Update(item);
        }

        public async Task<Response> UpdatePhoto(Guid photoId, byte[] photoData, string? description = null)
        {
            var photoResponse = await _unitOfWork.PetPhotoRepository.Get(photoId);
            if (!photoResponse.Success.Value || photoResponse.Item == null)
                return ResponseFactory.CreateFailedResponse("Pet photo not found");

            photoResponse.Item.PhotoData = photoData;
            if (description != null)
                photoResponse.Item.Description = description;
            photoResponse.Item.Date = DateTime.UtcNow;
            return await _unitOfWork.PetPhotoRepository.Update(photoResponse.Item);
        }

        public async Task<Response> AddPetPhoto(Guid petId, byte[] photoData, string? description = null)
        {
            var petResponse = await _unitOfWork.PetRepository.Get(petId);
            if (!petResponse.Success.Value || petResponse.Item == null)
                return ResponseFactory.CreateFailedResponse("Pet not found");

            var petPhoto = new PetPhoto
            {
                PetId = petId,
                Pet = petResponse.Item,
                PhotoData = photoData,
                Description = description,
                Date = DateTime.UtcNow
            };
            var validationResult = _validator.Validate(petPhoto);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();
            return await _unitOfWork.PetPhotoRepository.Insert(petPhoto);
        }

        public async Task<Response> UpdatePetPhoto(Guid petId, Guid photoId, byte[]? photoData = null, string? description = null)
        {
            var photoResponse = await _unitOfWork.PetPhotoRepository.Get(photoId);
            if (!photoResponse.Success.Value || photoResponse.Item == null || photoResponse.Item.PetId != petId)
                return ResponseFactory.CreateFailedResponse("Pet photo not found for this pet");

            if (photoData != null)
                photoResponse.Item.PhotoData = photoData;
            if (description != null)
                photoResponse.Item.Description = description;
            photoResponse.Item.Date = DateTime.UtcNow;
            var validationResult = _validator.Validate(photoResponse.Item);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();
            return await _unitOfWork.PetPhotoRepository.Update(photoResponse.Item);
        }
    }
}