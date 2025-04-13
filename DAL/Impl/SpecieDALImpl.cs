using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class SpecieDALImpl : BaseDAL<Specie>, ISpecieDAL
    {
        public SpecieDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Specie>> logger) : base(dbContext, logger)
        {
        }
    }
}