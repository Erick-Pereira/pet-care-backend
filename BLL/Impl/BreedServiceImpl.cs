using BLL.Interfaces;
using BLL.Validation;
using Commons.Constants;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class BreedServiceImpl : IBreedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BreedValidator validator;

        public BreedServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new BreedValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.BreedRepository.Delete(id);
        }

        public async Task<SingleResponse<Breed>> Get(Guid id)
        {
            return await _unitOfWork.BreedRepository.Get(id);
        }

        public async Task<DataResponse<Breed>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.BreedRepository.Get(skip, take, filter);
        }

        public async Task<Response> Insert(Breed item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.BreedRepository.Insert(item);
        }

        public async Task<Response> Update(Breed item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.BreedRepository.Update(item);
        }

        public async Task<SingleResponse<Breed>> ToggleActive(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.BreedRepository.Get(id);
                if (!entity.Success == true || entity.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Breed>(ErrorMessages.BreedNotFound);

                entity.Item.Active = !entity.Item.Active;
                var updateResponse = await _unitOfWork.BreedRepository.Update(entity.Item);

                if (!updateResponse.Success == true)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Breed>(updateResponse.Message ?? ErrorMessages.ErrorTogglingBreedStatus);

                return ResponseFactory.CreateSuccessSingleResponse(entity.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Breed>(ErrorMessages.ErrorTogglingBreedStatus, ex);
            }
        }
    }
}