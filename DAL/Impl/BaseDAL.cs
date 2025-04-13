using Commons.Responses;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public abstract class BaseDAL<T> where T : Entity
    {
        protected readonly DataBaseDbContext _dbContext;
        private readonly ILogger<BaseDAL<T>> _logger;

        protected BaseDAL(DataBaseDbContext dbContext, ILogger<BaseDAL<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public virtual async Task<Response> Insert(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailedResponse(ex);
            }
        }

        public virtual async Task<Response> Update(T entity)
        {
            try
            {
                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailedResponse(ex);
            }
        }

        public virtual async Task<Response> Delete(Guid id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed: Entity {EntityId} not found.", id);
                return ResponseFactory.CreateFailedResponseNotFoundId();
            }

            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Entity {EntityId} deleted successfully.", id);
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity {EntityId}.", id);
                return ResponseFactory.CreateFailedResponse(ex);
            }
        }

        public virtual async Task<SingleResponse<T>> Get(Guid id)
        {
            try
            {
                var entity = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                if (entity == null)
                {
                    _logger.LogWarning("Get failed: Entity {EntityId} not found.", id);
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<T>();
                }

                _logger.LogInformation("Entity {EntityId} retrieved successfully.", id);
                return ResponseFactory.CreateSuccessSingleResponse(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity {EntityId}.", id);
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<T>(ex);
            }
        }

        public virtual async Task<DataResponse<T>> Get(int skip, int take)
        {
            try
            {
                var entities = await _dbContext.Set<T>().Skip(skip).Take(take).ToListAsync();
                return ResponseFactory.CreateInstance().CreateSuccessDataResponse(entities);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<T>(ex);
            }
        }
    }
}