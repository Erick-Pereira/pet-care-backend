using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class UserDALImpl : BaseDAL<User>, IUserDAL
    {
        private readonly ILogger<BaseDAL<User>> _logger;
        private readonly DataBaseDbContext _db;

        public UserDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<User>> logger) : base(dbContext, logger)
        {
            _db = dbContext;
            _logger = logger;
        }

        public async Task<DataResponse<User>> Get(int skip, int take, string? createdBy = null, bool? active = null)
        {
            try
            {
                var query = _db.User.AsQueryable();

                if (!string.IsNullOrEmpty(createdBy))
                    query = query.Where(u => u.CreatedBy == createdBy);

                if (active.HasValue)
                    query = query.Where(u => u.Active == active.Value);

                var users = await query
                    .OrderBy(u => u.CreatedAt)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                _logger.LogInformation("{Count} users retrieved successfully with filters (skip: {Skip}, take: {Take}, createdBy: {CreatedBy}, active: {Active}).", users.Count, skip, take, createdBy, active);
                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<User>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users with filters (skip: {Skip}, take: {Take}, createdBy: {CreatedBy}, active: {Active}).", skip, take, createdBy, active);
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<User>(ex);
            }
        }
    }
}