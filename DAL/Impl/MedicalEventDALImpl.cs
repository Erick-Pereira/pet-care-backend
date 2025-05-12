using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class MedicalEventDALImpl : BaseDAL<MedicalEvent>, IMedicalEventDAL
    {
        public MedicalEventDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<MedicalEvent>> logger) : base(dbContext, logger)
        {
        }
    }
}