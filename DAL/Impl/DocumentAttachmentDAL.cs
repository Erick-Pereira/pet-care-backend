using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class DocumentAttachmentDAL : BaseDAL<DocumentAttachment>, IDocumentAttachmentDAL
    {
        public DocumentAttachmentDAL(DataBaseDbContext dbContext, ILogger<BaseDAL<DocumentAttachment>> logger) : base(dbContext, logger)
        {
        }
    }
}