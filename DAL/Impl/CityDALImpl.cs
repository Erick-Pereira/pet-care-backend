using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class CityDALImpl : BaseDAL<City>, ICityDAL
    {
        public CityDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<City>> logger) : base(dbContext, logger)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Insert(City item)
        {
            try
            {
                _db.Set<City>().Add(item);
                await _db.SaveChangesAsync();
                _logger.LogInformation("City {CityId} inserted successfully.", item.Id);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting city {CityId}.", item.Id);
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public Task<Response> Update(City item)
        {
            throw new NotImplementedException();
        }

        // Outros métodos (Get, Update, Delete) seguem o mesmo padrão...
    }
}