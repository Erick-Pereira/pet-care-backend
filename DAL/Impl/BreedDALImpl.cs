using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class BreedDALImpl : BaseDAL<Breed>, IBreedDAL
    {
        public BreedDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Breed>> logger) : base(dbContext, logger)
        {
        }

        protected override IQueryable<Breed> AddIncludes(IQueryable<Breed> query)
        {
            return query.Include(b => b.Specie);
        }
    }
}