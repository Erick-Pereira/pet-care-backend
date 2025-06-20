using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class StateDALImpl : BaseDAL<State>, IStateDAL
    {
        public StateDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<State>> logger) : base(dbContext, logger)
        {
        }

        public async Task<SingleResponse<State>> FindByAbbreviation(string abreviation)
        {
            try
            {
                var state = await _dbContext.Set<State>()
                    .FirstOrDefaultAsync(s => s.Abbreviation.ToLower() == abreviation.ToLower());

                if (state == null)
                {
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponse<State>($"State with abreviation '{abreviation}' not found");
                }

                return ResponseFactory.CreateSuccessSingleResponse(state);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error finding state with abreviation '{abreviation}'");
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<State>("Error finding state", ex);
            }
        }
    }
}