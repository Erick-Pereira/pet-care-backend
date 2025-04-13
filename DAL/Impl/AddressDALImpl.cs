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
        }
    }
}