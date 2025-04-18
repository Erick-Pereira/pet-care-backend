﻿using BLL.Interfaces;
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
        private readonly PetValidator validator;

        public PetServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new PetValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.PetRepository.Delete(id);
        }

        public async Task<SingleResponse<Pet>> Get(Guid id)
        {
            return await _unitOfWork.PetRepository.Get(id);
        }

        public async Task<DataResponse<Pet>> Get(int skip, int take)
        {
            return await _unitOfWork.PetRepository.Get(skip, take);
        }

        public async Task<Response> Insert(Pet item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.PetRepository.Insert(item);
        }

        public async Task<Response> Update(Pet item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.PetRepository.Update(item);
        }

        public async Task<Response> RegisterPetWithOwner(PetRegistrationRequest request)
        {
            // Validate the pet and owner
            var petValidationResult = validator.Validate(request.Pet);
            if (!petValidationResult.IsValid)
            {
                return petValidationResult.ToResponse();
            }

            // Insert owner into the database
            var ownerResponse = await _unitOfWork.UserRepository.Insert(request.Owner);
            if ((bool)!ownerResponse.Success)
            {
                return ownerResponse;
            }

            request.Pet.UserId = request.Owner.Id;

            // Insert pet into the database
            var petResponse = await _unitOfWork.PetRepository.Insert(request.Pet);
            return (bool)!petResponse.Success ? petResponse : ResponseFactory.CreateInstance().CreateSuccessResponse("Pet and owner registered successfully.");
        }
    }
}