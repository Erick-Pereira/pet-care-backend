using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class ExamDALImpl : BaseDAL<Exam>, IExamDAL
    {
        public ExamDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<Exam>> logger) : base(dbContext, logger)
        {
        }
    }
}