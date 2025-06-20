using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Impl
{
    public class MedicalEventDALImpl : BaseDAL<MedicalEvent>, IMedicalEventDAL
    {
        public MedicalEventDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<MedicalEvent>> logger) : base(dbContext, logger)
        {
        }

        protected override IQueryable<MedicalEvent> AddIncludes(IQueryable<MedicalEvent> query)
        {
            return query.Include(m => m.Pet).Include(m => m.Attachments);
        }
    }
}