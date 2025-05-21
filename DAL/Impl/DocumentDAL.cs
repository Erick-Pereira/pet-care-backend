using Commons.Interfaces;
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
    public class DocumentDAL : BaseDAL<Document>, IDocumentDAL
    {
        public DocumentDAL(DataBaseDbContext dbContext, ILogger<BaseDAL<Document>> logger) : base(dbContext, logger)
        {
        }
    }
}