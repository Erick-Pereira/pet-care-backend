using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class MedicalAttachmentDALImpl : BaseDAL<MedicalAttachment>, IMedicalAttachmentDAL
    {
        public MedicalAttachmentDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<MedicalAttachment>> logger) : base(dbContext, logger)
        {
        }
    }
}