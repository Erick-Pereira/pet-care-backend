using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class StateDALImpl : IStateDAL
    {
        private readonly DataBaseDbContext _db;
        private readonly ILogger<StateDALImpl> _logger;

        public StateDALImpl(DataBaseDbContext db, ILogger<StateDALImpl> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<Response> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<State>> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<State>> Get(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> Insert(State item)
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