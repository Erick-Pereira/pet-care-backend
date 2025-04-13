using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class StateDALImpl : BaseDAL<State>, IStateDAL
    {
        public StateDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<State>> logger) : base(dbContext, logger)
        {
        }
    }
}