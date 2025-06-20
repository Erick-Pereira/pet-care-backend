using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Impl
{
    public class PetDALImpl : BaseDAL<Pet>, IPetDAL
    {
        public PetDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Pet>> logger) : base(dbContext, logger)
        {
        }

        protected override IQueryable<Pet> AddIncludes(IQueryable<Pet> query)
        {
            return query
                .Include(p => p.Owner)
                .Include(p => p.Breed)
                .Include(p => p.Specie);
        }
    }
}