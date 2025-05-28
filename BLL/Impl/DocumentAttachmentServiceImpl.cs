using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class DocumentAttachmentServiceImpl : IDocumentAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DocumentAttachmentValidator validator;

        public DocumentAttachmentServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new DocumentAttachmentValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.DocumentAttachmentRepository.Delete(id);
        }

        public async Task<SingleResponse<DocumentAttachment>> Get(Guid id)
        {
            return await _unitOfWork.DocumentAttachmentRepository.Get(id);
        }

        public async Task<DataResponse<DocumentAttachment>> Get(int skip, int take)
        {
            return await _unitOfWork.DocumentAttachmentRepository.Get(skip, take);
        }

        public async Task<Response> Insert(DocumentAttachment item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.DocumentAttachmentRepository.Insert(item);
        }

        public async Task<Response> Update(DocumentAttachment item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.DocumentAttachmentRepository.Update(item);
        }
    }
}