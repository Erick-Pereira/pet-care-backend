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
        private readonly ICityService _cityService;

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

        public async Task<SingleResponse<Neighborhood>> UpdateReturnObject(Neighborhood item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToSingleResponse<Neighborhood>();
            }

            return await _unitOfWork.NeighborhoodRepository.UpdateReturnObject(item);
        }

        public async Task<SingleResponse<Neighborhood>> InsertReturnObject(Neighborhood item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToSingleResponse<Neighborhood>();
            }

            return await _unitOfWork.NeighborhoodRepository.InsertReturnObject(item);
        }

        public async Task<SingleResponse<Neighborhood>> FindOrCreateNew(Neighborhood neighborhood)
        {
            var addressResponse = await _unitOfWork.NeighborhoodRepository.FindByNeighborhood(neighborhood);
            if (addressResponse.Success.GetValueOrDefault() && addressResponse.Item != null)
                return addressResponse;

            var cityResponse = await _cityService.FindOrCreateNew(neighborhood.City);
            if (!cityResponse.Success.GetValueOrDefault() || cityResponse.Item == null)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>(cityResponse.Message);

            neighborhood.City = cityResponse.Item;
            neighborhood.CityId = cityResponse.Item.Id;
            return await _unitOfWork.NeighborhoodRepository.InsertReturnObject(neighborhood);
        }

        public async Task<SingleResponse<Neighborhood>> FindOrCreateOrSwitch(Neighborhood neighborhood)
        {
            var neighborhoodResult = await _unitOfWork.NeighborhoodRepository.FindByNeighborhood(neighborhood);

            int addressesUsingOldNeighborhood = await _unitOfWork.AddressRepository.CountAllByNeighborhoodId(neighborhood.Id);

            if (addressesUsingOldNeighborhood == 1)
            {
                if (neighborhoodResult.Success.GetValueOrDefault() && neighborhoodResult.Item != null)
                {
                    await _unitOfWork.NeighborhoodRepository.Delete(neighborhood.Id);
                    return neighborhoodResult;
                }

                var cityResponse = await _cityService.FindOrCreateOrSwitch(neighborhood.City);
                if (!cityResponse.Success.GetValueOrDefault() || cityResponse.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>(cityResponse.Message);

                neighborhood.City = cityResponse.Item;
                neighborhood.CityId = neighborhood.City.Id;
                return await _unitOfWork.NeighborhoodRepository.UpdateReturnObject(neighborhood);
            }
            else
            {
                if (neighborhoodResult.Success.GetValueOrDefault() && neighborhoodResult.Item != null)
                    return neighborhoodResult;

                var cityResponse = await _cityService.FindOrCreateOrSwitch(neighborhood.City);
                if (!cityResponse.Success.GetValueOrDefault() || cityResponse.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>(cityResponse.Message);

                neighborhood.City = cityResponse.Item;
                neighborhood.CityId = neighborhood.City.Id;
                return await _unitOfWork.NeighborhoodRepository.InsertReturnObject(neighborhood);
            }
        }
    }
}