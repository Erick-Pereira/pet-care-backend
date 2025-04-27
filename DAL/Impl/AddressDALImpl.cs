using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class AddressDALImpl : BaseDAL<Address>, IAddressDAL
    {
        public AddressDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Address>> logger) : base(dbContext, logger)
        {
        }

        public async Task<SingleResponse<Address>> FindByAddress(Address address)
        {
            try
            {
                var existingAddress = _dbContext.Set<Address>()
                    .FirstOrDefault(a =>
                        a.Street == address.Street &&
                        a.Number == address.Number &&
                        a.Complement == address.Complement &&
                        a.NeighborhoodId == address.NeighborhoodId);

                if (existingAddress == null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>("Address not found");
                }

                return ResponseFactory.CreateSuccessSingleResponse(existingAddress);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>("Error finding address", ex);
            }
        }

        public async Task<SingleResponse<Address>> InsertReturnObject(Address address)
        {
            try
            {
                var result = await _dbContext.Set<Address>().AddAsync(address);
                await _dbContext.SaveChangesAsync();

                return ResponseFactory.CreateSuccessSingleResponse(result.Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting address");
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>("Error inserting address", ex);
            }
        }

        public async Task<SingleResponse<Address>> UpdateReturnObject(Address address)
        {
            try
            {
                var existingAddress = await _dbContext.Set<Address>().FindAsync(address.Id);
                if (existingAddress == null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>("Address not found");
                }

                _dbContext.Entry(existingAddress).CurrentValues.SetValues(address);
                await _dbContext.SaveChangesAsync();

                return ResponseFactory.CreateSuccessSingleResponse(existingAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating address");
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<Address>("Error updating address", ex);
            }
        }

        public async Task<int> CountAllByNeighborhoodId(Guid neighborhoodId)
        {
            try
            {
                return await _dbContext.Set<Address>()
                    .CountAsync(a => a.NeighborhoodId == neighborhoodId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error counting addresses by neighborhood ID {neighborhoodId}");
                return 0;
            }
        }
    }
}