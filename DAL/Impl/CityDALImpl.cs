using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class CityDALImpl : BaseDAL<City>, ICityDAL
    {
        public CityDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<City>> logger)
            : base(dbContext, logger)
        {
        }

        public async Task<SingleResponse<City>> InsertReturnObject(City city)
        {
            try
            {
                var result = await _dbContext.Set<City>().AddAsync(city);
                await _dbContext.SaveChangesAsync();

                return ResponseFactory.CreateSuccessSingleResponse(result.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting city");
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("Error inserting city", ex);
            }
        }

        public async Task<SingleResponse<City>> UpdateReturnObject(City city)
        {
            try
            {
                var existingCity = await _dbContext.Set<City>().FindAsync(city.Id);
                if (existingCity == null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("City not found");
                }

                _dbContext.Entry(existingCity).CurrentValues.SetValues(city);
                await _dbContext.SaveChangesAsync();

                return ResponseFactory.CreateSuccessSingleResponse(existingCity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating city");
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("Error updating city", ex);
            }
        }

        public async Task<SingleResponse<City>> FindByName(City city)
        {
            try
            {
                var existingCity = await _dbContext.Set<City>()
                    .FirstOrDefaultAsync(c =>
                        c.Name.ToLower() == city.Name.ToLower() &&
                        c.StateId == city.StateId);

                if (existingCity == null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("City not found");
                }

                return ResponseFactory.CreateSuccessSingleResponse(existingCity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding city by name");
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<City>("Error finding city", ex);
            }
        }

        protected override IQueryable<City> AddIncludes(IQueryable<City> query)
        {
            return query.Include(c => c.State);
        }
    }
}