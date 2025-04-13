using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class CityDALImpl : BaseDAL<City>, ICityDAL
    {
        public CityDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<City>> logger) : base(dbContext, logger)
        {
        }
    }
}