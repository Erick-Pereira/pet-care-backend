using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class NeighborhoodDALImpl : BaseDAL<Neighborhood>, INeighborhoodDAL
    {
        public NeighborhoodDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Neighborhood>> logger) : base(dbContext, logger)
        {
        }
    }
}