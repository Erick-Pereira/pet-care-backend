using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class DiagnosisServiceImpl : IDiagnosisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DiagnosisValidator validator;

        public DiagnosisServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new DiagnosisValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.DiagnosisRepository.Delete(id);
        }

        public async Task<SingleResponse<Diagnosis>> Get(Guid id)
        {
            return await _unitOfWork.DiagnosisRepository.Get(id);
        }

        public async Task<DataResponse<Diagnosis>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.DiagnosisRepository.Get(skip, take, filter);
        }

        public async Task<Response> Insert(Diagnosis item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.DiagnosisRepository.Insert(item);
        }

        public async Task<Response> Update(Diagnosis item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.DiagnosisRepository.Update(item);
        }
    }
}