using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class MedicalEventServiceImpl : IMedicalEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MedicalEventValidator validator;

        public MedicalEventServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new MedicalEventValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.MedicalEventRepository.Delete(id);
        }

        public async Task<SingleResponse<MedicalEvent>> Get(Guid id)
        {
            return await _unitOfWork.MedicalEventRepository.Get(id);
        }

        public async Task<DataResponse<MedicalEvent>> Get(int skip, int take)
        {
            return await _unitOfWork.MedicalEventRepository.Get(skip, take);
        }

        public async Task<Response> Insert(MedicalEvent item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.MedicalEventRepository.Insert(item);
        }

        public async Task<Response> Update(MedicalEvent item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.MedicalEventRepository.Update(item);
        }
    }
}