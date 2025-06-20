using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class UserDALImpl : BaseDAL<User>, IUserDAL
    {
        private readonly DataBaseDbContext _db;

        public UserDALImpl(DataBaseDbContext dbContext, ILogger<BaseDAL<User>> logger) : base(dbContext, logger)
        {
            _db = dbContext;
        }

        public async Task<DataResponse<User>> Get(int skip, int take, bool? active = null)
        {
            try
            {
                var query = _db.User.AsQueryable();

                if (active.HasValue)
                    query = query.Where(u => u.Active == active.Value);

                var users = await query
                    .OrderBy(u => u.CreatedAt)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                _logger.LogInformation("{Count} users retrieved successfully with filters (skip: {Skip}, take: {Take}, active: {Active}).", users.Count, skip, take, active);
                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<User>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users with filters (skip: {Skip}, take: {Take}, active: {Active}).", skip, take, active);
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<User>(ex);
            }
        }

        public async Task<int> CountAllByAddressId(Guid addressId)
        {
            try
            {
                return await _dbContext.Set<User>()
                    .CountAsync(u => u.AddressId == addressId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error counting users by address ID {addressId}");
                return 0;
            }
        }

        public async Task<SingleResponse<User>> GetByEmail(string email)
        {
            try
            {
                var user = await _dbContext.Set<User>()
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

                return user == null
                    ? ResponseFactory.CreateInstance().CreateFailedSingleResponse<User>("User not found")
                    : ResponseFactory.CreateSuccessSingleResponse(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponse<User>("Error retrieving user by email", ex);
            }
        }

        protected override IQueryable<User> AddIncludes(IQueryable<User> query)
        {
            return query.Include(u => u.Address);
        }
    }
}