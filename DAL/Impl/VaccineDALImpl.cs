using Commons.Interfaces;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class VaccineDALImpl : BaseDAL<Vaccine>, IVaccineDAL
    {
        public VaccineDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Vaccine>> logger) : base(dbContext, logger)
        {
        }
    }
}