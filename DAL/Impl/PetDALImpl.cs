using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class PetDALImpl : BaseDAL<Pet>, IPetDAL
    {
        public PetDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Pet>> logger) : base(dbContext, logger)
        {
        }
    }
}