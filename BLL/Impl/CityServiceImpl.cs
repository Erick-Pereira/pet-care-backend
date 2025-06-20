using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class CityServiceImpl : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStateService _stateService;
        private readonly CityValidator validator;

        public CityServiceImpl(IUnitOfWork unitOfWork, IStateService stateService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
            validator = new CityValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.CityRepository.Delete(id);
        }

        public async Task<SingleResponse<City>> Get(Guid id)
        {
            return await _unitOfWork.CityRepository.Get(id);
        }

        public async Task<DataResponse<City>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.CityRepository.Get(skip, take, filter);
        }

        public async Task<Response> Insert(City item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.CityRepository.Insert(item);
        }

        public async Task<Response> Update(City item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.CityRepository.Update(item);
        }

        public async Task<SingleResponse<City>> InsertReturnObject(City item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToSingleResponse<City>();
            }

            return await _unitOfWork.CityRepository.InsertReturnObject(item);
        }

        public async Task<SingleResponse<City>> UpdateReturnObject(City item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToSingleResponse<City>();
            }

            return await _unitOfWork.CityRepository.UpdateReturnObject(item);
        }

        public async Task<SingleResponse<City>> FindOrCreateNew(City city)
        {
            var cityResponse = await _unitOfWork.CityRepository.FindByName(city);

            if (cityResponse.Success == true && cityResponse.Item != null)
            {
                var stateResult = await _stateService.Get(cityResponse.Item.StateId);
                if (stateResult.Item != null && city.State.Abbreviation.ToLower() == stateResult.Item.Abbreviation.ToLower())
                {
                    return cityResponse;
                }
            }

            var stateResponse = await _stateService.FindByAbbreviation(city.State.Abbreviation);
            if (!stateResponse.Success == true || stateResponse.Item == null)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>(stateResponse.Message ?? "State response message was null.");

            city.State = stateResponse.Item;
            if (stateResponse.Item.Id == null)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("State Id is null.");
            city.StateId = stateResponse.Item.Id;
            return await _unitOfWork.CityRepository.InsertReturnObject(city);
        }

        public async Task<SingleResponse<City>> FindOrCreateOrSwitch(City city)
        {
            var cityResult = await _unitOfWork.CityRepository.FindByName(city);

            if (city.Id == null)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("City Id is null.");

            int neighborhoodsUsingOldCity = await _unitOfWork.NeighborhoodRepository.CountAllByCityId(city.Id);

            if (neighborhoodsUsingOldCity == 1)
            {
                if (cityResult.Success == true && cityResult.Item != null)
                {
                    var stateResult = await _stateService.Get(cityResult.Item.StateId);

                    if (stateResult.Item != null && city.State.Abbreviation.ToLower() == stateResult.Item.Abbreviation.ToLower())
                    {
                        await _unitOfWork.CityRepository.Delete(city.Id);
                        return cityResult;
                    }
                }

                var stateResponse = await _stateService.FindByAbbreviation(city.State.Abbreviation);
                if (!stateResponse.Success == true || stateResponse.Item == null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>(stateResponse.Message ?? "State response message was null.");
                }
                city.State = stateResponse.Item;
                if (stateResponse.Item.Id == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("State Id is null.");
                city.StateId = stateResponse.Item.Id;
                return await _unitOfWork.CityRepository.UpdateReturnObject(city);
            }
            else
            {
                if (cityResult.Success == true && cityResult.Item != null)
                {
                    var stateResult = await _stateService.Get(cityResult.Item.StateId);
                    if (stateResult.Item != null && city.State.Abbreviation.Equals(stateResult.Item.Abbreviation, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return cityResult;
                    }
                }

                var stateResponse = await _stateService.FindByAbbreviation(city.State.Abbreviation);
                if (!stateResponse.Success == true || stateResponse.Item == null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>(stateResponse.Message ?? "State response message was null.");
                }

                city.State = stateResponse.Item;
                if (stateResponse.Item.Id == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("State Id is null.");
                city.StateId = stateResponse.Item.Id;
                return await _unitOfWork.CityRepository.InsertReturnObject(city);
            }
        }
    }
}