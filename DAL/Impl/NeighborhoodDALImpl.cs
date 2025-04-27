using Commons.Responses;
using DAL;
using DAL.Impl;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class NeighborhoodDALImpl : BaseDAL<Neighborhood>, INeighborhoodDAL
{
    public NeighborhoodDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Neighborhood>> logger) : base(dbContext, logger)
    {
    }

    public async Task<SingleResponse<Neighborhood>> FindByNeighborhood(Neighborhood neighborhood)
    {
        try
        {
            var existingNeighborhood = await _dbContext.Set<Neighborhood>()
                .FirstOrDefaultAsync(n =>
                    n.Name == neighborhood.Name &&
                    n.CityId == neighborhood.CityId);

            if (existingNeighborhood == null)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Neighborhood not found");
            }

            return ResponseFactory.CreateSuccessSingleResponse(existingNeighborhood);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding neighborhood");
            return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Error finding neighborhood", ex);
        }
    }

    public async Task<SingleResponse<Neighborhood>> InsertReturnObject(Neighborhood neighborhood)
    {
        try
        {
            var result = await _dbContext.Set<Neighborhood>().AddAsync(neighborhood);
            await _dbContext.SaveChangesAsync();

            return ResponseFactory.CreateSuccessSingleResponse(result.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting neighborhood");
            return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Error inserting neighborhood", ex);
        }
    }

    public async Task<SingleResponse<Neighborhood>> UpdateReturnObject(Neighborhood neighborhood)
    {
        try
        {
            var existingAddress = await _dbContext.Set<Neighborhood>().FindAsync(neighborhood.Id);
            if (existingAddress == null)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Address not found");
            }

            _dbContext.Entry(existingAddress).CurrentValues.SetValues(neighborhood);
            await _dbContext.SaveChangesAsync();

            return ResponseFactory.CreateSuccessSingleResponse(existingAddress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating address");
            return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Error updating address", ex);
        }
    }

    public async Task<int> CountAllByCityId(Guid cityId)
    {
        try
        {
            return await _dbContext.Set<Neighborhood>()
                .CountAsync(n => n.CityId == cityId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error counting neighborhoods by city ID {cityId}");
            return 0;
        }


        public async Task<SingleResponse<Neighborhood>> FindByNeighborhood(Neighborhood neighborhood)
    {
        try
        {
            var existingNeighborhood = await _dbContext.Set<Neighborhood>()
                .FirstOrDefaultAsync(n =>
                    n.Name == neighborhood.Name &&
                    n.CityId == neighborhood.CityId);

            if (existingNeighborhood == null)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Neighborhood not found");
            }

            return ResponseFactory.CreateSuccessSingleResponse(existingNeighborhood);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding neighborhood");
            return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Neighborhood>("Error finding neighborhood", ex);
        }
    }

    public Task<SingleResponse<Neighborhood>> InsertReturnObject(Neighborhood neighborhood)
    {
        throw new NotImplementedException();
    }

    public Task<SingleResponse<Neighborhood>> UpdateReturnObject(Neighborhood neighborhood)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAllByCityId(Guid cityId)
    {
        try
        {
            return await _dbContext.Set<Neighborhood>()
                .CountAsync(n => n.CityId == cityId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error counting neighborhoods by city ID {cityId}");
            return 0;
        }
    }
}
}