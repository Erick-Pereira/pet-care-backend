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

        public NeighborhoodServiceImpl(IUnitOfWork unitOfWork, ICityService cityService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
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
            var neighborhoodResult = await _unitOfWork.NeighborhoodRepository.FindByNeighborhood(neighborhood);
            if (neighborhoodResult.Success == true && neighborhoodResult.Item != null)
            {
                var cityResult = await _cityService.Get(neighborhoodResult.Item.CityId);
                if (neighborhood.City != null && !string.IsNullOrEmpty(neighborhood.City.Name) &&
                    cityResult.Item != null && !string.IsNullOrEmpty(cityResult.Item.Name) &&
                    neighborhood.City.Name.ToLower() == cityResult.Item.Name.ToLower())
                {
                    return neighborhoodResult;
                }
            }

            if (neighborhood.City == null)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("City cannot be null.");
            }

            var cityResponse = await _cityService.FindOrCreateNew(neighborhood.City);
            if (!cityResponse.Success == true || cityResponse.Item == null)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>(cityResponse.Message ?? "Unknown error.");

            neighborhood.City = cityResponse.Item;
            if (!cityResponse.Item.Id.HasValue)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("City Id is null.");
            neighborhood.CityId = cityResponse.Item.Id.Value;
            return await _unitOfWork.NeighborhoodRepository.InsertReturnObject(neighborhood);
        }

        public async Task<SingleResponse<Neighborhood>> FindOrCreateOrSwitch(Neighborhood neighborhood)
        {
            var neighborhoodResult = await _unitOfWork.NeighborhoodRepository.FindByNeighborhood(neighborhood);

            if (!neighborhood.Id.HasValue)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Neighborhood Id is null.");

            int addressesUsingOldNeighborhood = await _unitOfWork.AddressRepository.CountAllByNeighborhoodId(neighborhood.Id.Value);

            if (addressesUsingOldNeighborhood == 1)
            {
                if (neighborhoodResult.Success == true && neighborhoodResult.Item != null)
                {
                    var cityResult = await _cityService.Get(neighborhoodResult.Item.CityId);
                    if (neighborhood.City != null && !string.IsNullOrEmpty(neighborhood.City.Name) &&
                        cityResult.Item != null && !string.IsNullOrEmpty(cityResult.Item.Name) &&
                        neighborhood.City.Name.Equals(cityResult.Item.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        await _unitOfWork.NeighborhoodRepository.Delete(neighborhood.Id.Value);
                        return neighborhoodResult;
                    }
                }

                if (neighborhood.City == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("City cannot be null.");

                var cityResponse = await _cityService.FindOrCreateOrSwitch(neighborhood.City);
                if (!cityResponse.Success == true || cityResponse.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>(cityResponse.Message ?? "Unknown error.");

                neighborhood.City = cityResponse.Item;
                if (!cityResponse.Item.Id.HasValue)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("City Id is null.");
                neighborhood.CityId = cityResponse.Item.Id.Value;
                return await _unitOfWork.NeighborhoodRepository.UpdateReturnObject(neighborhood);
            }
            else
            {
                if (neighborhoodResult.Success == true && neighborhoodResult.Item != null)
                {
                    var cityResult = await _cityService.Get(neighborhoodResult.Item.CityId);
                    if (neighborhood.City != null && !string.IsNullOrEmpty(neighborhood.City.Name) &&
                        cityResult.Item != null && !string.IsNullOrEmpty(cityResult.Item.Name) &&
                        neighborhood.City.Name.ToLower() == cityResult.Item.Name.ToLower())
                    {
                        return neighborhoodResult;
                    }
                }

                if (neighborhood.City == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("City cannot be null.");

                var cityResponse = await _cityService.FindOrCreateOrSwitch(neighborhood.City);
                if (!cityResponse.Success == true || cityResponse.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>(cityResponse.Message ?? "Unknown error.");

                neighborhood.City = cityResponse.Item;
                if (!neighborhood.City.Id.HasValue)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("City Id is null.");
                neighborhood.CityId = neighborhood.City.Id.Value;
                return await _unitOfWork.NeighborhoodRepository.InsertReturnObject(neighborhood);
            }
        }
    }
}