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

        public async Task<DataResponse<PetPhoto>> Get(int skip, int take)
        {
            return await _unitOfWork.PetPhotoRepository.Get(skip, take);
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
    }
}