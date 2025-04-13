using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class StateDALImpl : BaseDAL<State>, IStateDAL
    {
        public StateDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<State>> logger) : base(dbContext, logger)
        {
            try
            {
                _db.Set<State>().Add(item);
                await _db.SaveChangesAsync();
                _logger.LogInformation("State {StateId} inserted successfully.", item.Id);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting state {StateId}.", item.Id);
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public Task<Response> Update(State item)
        {
            throw new NotImplementedException();
        }
    }
}