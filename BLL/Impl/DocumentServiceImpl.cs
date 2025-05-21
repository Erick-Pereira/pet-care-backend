using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class DocumentServiceImpl : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DocumentValidator validator;

        public DocumentServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new DocumentValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.DocumentRepository.Delete(id);
        }

        public async Task<SingleResponse<Document>> Get(Guid id)
        {
            return await _unitOfWork.DocumentRepository.Get(id);
        }

        public async Task<DataResponse<Document>> Get(int skip, int take)
        {
            return await _unitOfWork.DocumentRepository.Get(skip, take);
        }

        public async Task<Response> Insert(Document item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.DocumentRepository.Insert(item);
        }

        public async Task<Response> Update(Document item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.DocumentRepository.Update(item);
        }
    }
}