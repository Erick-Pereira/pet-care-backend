using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class NeighborhoodServiceImpl : INeighborhoodService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly NeighborhoodValidator validator = new NeighborhoodValidator();

        public NeighborhoodServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.NeighborhoodRepository.Delete(id);
        }

        public async Task<SingleResponse<Neighborhood>> Get(Guid id)
        {
            return await _unitOfWork.NeighborhoodRepository.Get(id);
        }

        public async Task<DataResponse<Neighborhood>> Get(int skip, int take)
        {
            return await _unitOfWork.NeighborhoodRepository.Get(skip, take);
        }

        public async Task<Response> Insert(Neighborhood item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.NeighborhoodRepository.Insert(item);
        }

        public async Task<Response> Update(Neighborhood item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.NeighborhoodRepository.Update(item);
        }
    }
}