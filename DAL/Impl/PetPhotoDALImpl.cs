using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class PetPhotoDALImpl : BaseDAL<PetPhoto>, IPetPhotoDAL
    {
        public PetPhotoDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<PetPhoto>> logger)
            : base(dbContext, logger)
        {
        }

        protected override IQueryable<PetPhoto> AddIncludes(IQueryable<PetPhoto> query)
        {
            return query.Include(p => p.Pet);
        }

        public async Task<DataResponse<PetPhoto>> GetByPetId(Guid petId)
        {
            try
            {
                var photos = await _dbContext.PetPhoto
                    .Where(p => p.PetId == petId)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();

                return ResponseFactory.CreateInstance().CreateSuccessDataResponse(photos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pet photos");
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<PetPhoto>(ex);
            }
        }
    }
}