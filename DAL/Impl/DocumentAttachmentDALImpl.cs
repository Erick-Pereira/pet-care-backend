using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class DocumentAttachmentDALImpl : BaseDAL<DocumentAttachment>, IDocumentAttachmentDAL
    {
        public DocumentAttachmentDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<DocumentAttachment>> logger) : base(dbContext, logger)
        {
        }
    }
}