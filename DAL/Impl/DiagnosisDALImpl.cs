using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class DiagnosisDALImpl : BaseDAL<Diagnosis>, IDiagnosisDAL
    {
        public DiagnosisDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Diagnosis>> logger) : base(dbContext, logger)
        {
        }
    }
}