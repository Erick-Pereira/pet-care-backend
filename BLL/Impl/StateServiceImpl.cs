using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class StateServiceImpl : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StateValidator validator;

        public StateServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new StateValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.StateRepository.Delete(id);
        }

        public async Task<SingleResponse<State>> Get(Guid id)
        {
            return await _unitOfWork.StateRepository.Get(id);
        }

        public async Task<DataResponse<State>> Get(int skip, int take)
        {
            return await _unitOfWork.StateRepository.Get(skip, take);
        }

        public async Task<Response> Insert(State item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.StateRepository.Insert(item);
        }

        public async Task<Response> Update(State item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.StateRepository.Update(item);
        }

        public async Task<SingleResponse<State>> FindByAbreviation(string abreviation)
        {
            if (string.IsNullOrWhiteSpace(abreviation))
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<State>("State abreviation cannot be empty");
            }

            return await _unitOfWork.StateRepository.FindByAbreviation(abreviation);
        }

        public async Task<SingleResponse<State>> ToggleActive(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.StateRepository.Get(id);
                if (!entity.Success == true || entity.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<State>("State not found");

                entity.Item.Active = !entity.Item.Active;
                var updateResponse = await _unitOfWork.StateRepository.Update(entity.Item);

                if (!updateResponse.Success == true)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<State>(updateResponse.Message ?? "Unknown error occurred while updating the state.");

                return ResponseFactory.CreateSuccessSingleResponse(entity.Item);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<State>("Error toggling state status", ex);
            }
        }
    }
}