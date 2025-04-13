using BLL.Interfaces;
using BLL.Validation;
using Commons.Extensions;
using Commons.Responses;
using DAL.UnitOfWork;
using Entities;

namespace BLL.Impl
{
    public class SpecieServiceImpl : ISpecieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SpecieValidator validator;

        public SpecieServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            validator = new SpecieValidator();
        }

        public async Task<Response> Delete(Guid id)
        {
            return await _unitOfWork.SpecieRepository.Delete(id);
        }

        public async Task<SingleResponse<Specie>> Get(Guid id)
        {
            return await _unitOfWork.SpecieRepository.Get(id);
        }

        public async Task<DataResponse<Specie>> Get(int skip, int take)
        {
            return await _unitOfWork.SpecieRepository.Get(skip, take);
        }

        public async Task<Response> Insert(Specie item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.SpecieRepository.Insert(item);
        }

        public async Task<Response> Update(Specie item)
        {
            var validationResult = validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponse();
            }

            return await _unitOfWork.SpecieRepository.Update(item);
        }
    }
}