using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class AddressServiceImpl : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INeighborhoodService _neighborhoodService;
        private readonly AddressValidator validator;

        public AddressServiceImpl(IUnitOfWork unitOfWork, INeighborhoodService neighborhoodService, AddressValidator validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _neighborhoodService = neighborhoodService ?? throw new ArgumentNullException(nameof(neighborhoodService));
            this.validator = validator;
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.AddressRepository.Delete(id);
        }

        public async Task<SingleResponse<Address>> Get(Guid id)
        {
            return await _unitOfWork.AddressRepository.Get(id);
        }

        public async Task<DataResponse<Address>> Get(int skip, int take)
        {
            return await _unitOfWork.AddressRepository.Get(skip, take);
        }

        public async Task<Response> Insert(Address item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.AddressRepository.Insert(item);
        }

        public async Task<Response> Update(Address item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.AddressRepository.Update(item);
        }

        public async Task<SingleResponse<Address>> UpdateReturnObject(Address item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToSingleResponse<Address>();
            }

            return await _unitOfWork.AddressRepository.UpdateReturnObject(item);
        }

        public async Task<SingleResponse<Address>> InsertReturnObject(Address item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToSingleResponse<Address>();
            }

            return await _unitOfWork.AddressRepository.InsertReturnObject(item);
        }

        public async Task<SingleResponse<Address>> FindOrCreateNew(Address address)
        {
            var addressResponse = await _unitOfWork.AddressRepository.FindByAddress(address);

            if (addressResponse.Success == true && addressResponse.Item != null)
            {
                var neghboorhoodResponse = await _neighborhoodService.Get(addressResponse.Item.NeighborhoodId);

                if (address.Neighborhood.Name.ToLower() == neghboorhoodResponse.Item.Name.ToLower())
                {
                    return addressResponse;
                }
            }

            var neighborhoodResponse = await _neighborhoodService.FindOrCreateNew(address.Neighborhood);
            if (!neighborhoodResponse.Success == true || neighborhoodResponse.Item == null)
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>(neighborhoodResponse.Message);

            address.Neighborhood = neighborhoodResponse.Item;
            address.NeighborhoodId = neighborhoodResponse.Item.Id;
            return await _unitOfWork.AddressRepository.InsertReturnObject(address);
        }

        public async Task<SingleResponse<Address>> FindOrCreateOrSwitch(Address address)
        {
            var addressResult = await _unitOfWork.AddressRepository.FindByAddress(address);

            int usersUsingOldAddress = await _unitOfWork.UserRepository.CountAllByAddressId(address.Id);

            if (usersUsingOldAddress == 1)
            {
                if (addressResult.Success == true && addressResult.Item != null)
                {
                    var neghboorhoodResponse = await _neighborhoodService.Get(addressResult.Item.NeighborhoodId);

                    if (address.Neighborhood.Name.ToLower() == neghboorhoodResponse.Item.Name.ToLower())
                    {
                        await _unitOfWork.AddressRepository.Delete(address.Id);
                        return addressResult;
                    }
                }

                var neighborhoodResponse = await _neighborhoodService.FindOrCreateOrSwitch(address.Neighborhood);
                if (!neighborhoodResponse.Success == true || neighborhoodResponse.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>(neighborhoodResponse.Message);

                address.Neighborhood = neighborhoodResponse.Item;
                return await _unitOfWork.AddressRepository.UpdateReturnObject(address);
            }
            else
            {
                if (addressResult.Success == true && addressResult.Item != null)
                {
                    var neghboorhoodResponse = await _neighborhoodService.Get(addressResult.Item.NeighborhoodId);

                    if (address.Neighborhood.Name.ToLower() == neghboorhoodResponse.Item.Name.ToLower())
                    {
                        return addressResult;
                    }
                }

                var neighborhoodResponse = await _neighborhoodService.FindOrCreateOrSwitch(address.Neighborhood);
                if (!neighborhoodResponse.Success == true || neighborhoodResponse.Item == null)
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>(neighborhoodResponse.Message);

                address.Neighborhood = neighborhoodResponse.Item;
                return await _unitOfWork.AddressRepository.InsertReturnObject(address);
            }
        }
    }
}