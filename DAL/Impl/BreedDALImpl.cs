using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class BreedDALImpl : BaseDAL<Breed>, IBreedDAL
    {
        public BreedDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Breed>> logger) : base(dbContext, logger)
        {
        }
    }
}