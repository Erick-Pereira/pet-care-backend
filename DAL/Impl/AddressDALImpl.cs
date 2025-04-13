using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class AddressDALImpl : BaseDAL<Address>, IAddressDAL
    {
        public AddressDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Address>> logger) : base(dbContext, logger)
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