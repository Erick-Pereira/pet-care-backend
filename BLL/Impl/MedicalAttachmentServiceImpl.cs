using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class MedicalAttachmentServiceImpl : IMedicalAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MedicalAttachmentValidator validator;

        public MedicalAttachmentServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new MedicalAttachmentValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.MedicalAttachmentRepository.Delete(id);
        }

        public async Task<SingleResponse<MedicalAttachment>> Get(Guid id)
        {
            return await _unitOfWork.MedicalAttachmentRepository.Get(id);
        }

        public async Task<DataResponse<MedicalAttachment>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.MedicalAttachmentRepository.Get(skip, take, filter);
        }

        public async Task<Response> Insert(MedicalAttachment item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.MedicalAttachmentRepository.Insert(item);
        }

        public async Task<Response> Update(MedicalAttachment item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.MedicalAttachmentRepository.Update(item);
        }
    }
}