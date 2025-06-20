using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class ExamServiceImpl : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ExamValidator validator;

        public ExamServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new ExamValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.ExamRepository.Delete(id);
        }

        public async Task<SingleResponse<Exam>> Get(Guid id)
        {
            return await _unitOfWork.ExamRepository.Get(id);
        }

        public async Task<DataResponse<Exam>> Get(int skip, int take, string? filter)
        {
            return await _unitOfWork.ExamRepository.Get(skip, take, filter);
        }

        public async Task<Response> Insert(Exam item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.ExamRepository.Insert(item);
        }

        public async Task<Response> Update(Exam item)
        {
            var validationResult = validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }
            return await _unitOfWork.ExamRepository.Update(item);
        }
    }
}