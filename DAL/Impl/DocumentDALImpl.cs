using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class DocumentDALImpl : BaseDAL<Document>, IDocumentDAL
    {
        public DocumentDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Document>> logger) : base(dbContext, logger)
        {
        }
    }
}