using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class CityDALImpl : ICityDAL
    {
        private readonly DataBaseDbContext _db;
        private readonly ILogger<CityDALImpl> _logger;

        public CityDALImpl(DataBaseDbContext db, ILogger<CityDALImpl> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<Response> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<City>> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<City>> Get(int skip, int take)
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