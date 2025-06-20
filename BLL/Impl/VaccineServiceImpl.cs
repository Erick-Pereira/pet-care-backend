using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class VaccineServiceImpl : IVaccineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VaccineValidator validator;

        public VaccineServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new VaccineValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.VaccineRepository.Delete(id);
        }

        public async Task<SingleResponse<Vaccine>> Get(Guid id)
        {
            return await _unitOfWork.VaccineRepository.Get(id);
        }

        public async Task<DataResponse<Vaccine>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.VaccineRepository.Get(skip, take, filter);
        }

        public async Task<Response> Insert(Vaccine item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.VaccineRepository.Insert(item);
        }

        public async Task<Response> Update(Vaccine item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.VaccineRepository.Update(item);
        }
    }
}