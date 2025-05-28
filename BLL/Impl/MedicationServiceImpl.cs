using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class MedicationServiceImpl : IMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MedicationValidator _validator;

        public MedicationServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _validator = new MedicationValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.MedicationRepository.Delete(id);
        }

        public async Task<SingleResponse<Medication>> Get(Guid id)
        {
            return await _unitOfWork.MedicationRepository.Get(id);
        }

        public async Task<DataResponse<Medication>> Get(int skip, int take)
        {
            return await _unitOfWork.MedicationRepository.Get(skip, take);
        }

        public async Task<DataResponse<Medication>> GetByMedicalEventId(Guid medicalEventId)
        {
            return await _unitOfWork.MedicationRepository.GetByMedicalEventId(medicalEventId);
        }

        public async Task<Response> Insert(Medication item)
        {
            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();

            return await _unitOfWork.MedicationRepository.Insert(item);
        }

        public async Task<Response> Update(Medication item)
        {
            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
                return validationResult.ToResponse();

            return await _unitOfWork.MedicationRepository.Update(item);
        }
    }
}