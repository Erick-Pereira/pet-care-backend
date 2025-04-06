using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class AddressDALImpl : IAddressDAL
    {
        private readonly DataBaseDbContext _db;
        private readonly ILogger<AddressDALImpl> _logger;

        public AddressDALImpl(DataBaseDbContext db, ILogger<AddressDALImpl> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<Response> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<Address>> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Address>> Get(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Insert(Address item)
        {
            try
            {
                _db.Set<Address>().Add(item);
                await _db.SaveChangesAsync();
                _logger.LogInformation("Address {AddressId} inserted successfully.", item.Id);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting address {AddressId}.", item.Id);
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public Task<Response> Update(Address item)
        {
            throw new NotImplementedException();
        }

        // Outros métodos (Get, Update, Delete) seguem o mesmo padrão...
    }
}