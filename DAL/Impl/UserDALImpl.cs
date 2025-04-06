using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    public class UserDALImpl : IUserDAL
    {
        private readonly DataBaseDbContext _db;
        private readonly ILogger<UserDALImpl> _logger;

        public UserDALImpl(DataBaseDbContext db, ILogger<UserDALImpl> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response> Delete(Guid id)
        {
            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Delete failed: User {UserId} not found.", id);
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            }
            try
            {
                _db.User.Remove(user);
                await _db.SaveChangesAsync();
                _logger.LogInformation("User {UserId} deleted successfully.", id);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}.", id);
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<User>> Get(Guid id)
        {
            try
            {
                var user = _db.User.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    _logger.LogWarning("Get failed: User {UserId} not found.", id);
                    return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<User>();
                }
                _logger.LogInformation("User {UserId} retrieved successfully.", id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user {UserId}.", id);
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<User>(ex);
            }
        }

        public async Task<DataResponse<User>> Get(int skip, int take)
        {
            try
            {
                var users = await _db.User
                    .OrderBy(u => u.CreatedAt)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                _logger.LogInformation("{Count} users retrieved successfully (skip: {Skip}, take: {Take}).", users.Count, skip, take);
                return ResponseFactory.CreateInstance().CreateSuccessDataResponse<User>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users (skip: {Skip}, take: {Take}).", skip, take);
                return ResponseFactory.CreateInstance().CreateFailedDataResponse<User>(ex);
            }
        }

        public async Task<DataResponse<User>> Get(int skip, int take, string createdBy = null, bool? active = null)
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

        public async Task<Response> Insert(User item)
        {
            try
            {
                _db.User.Add(item);
                await _db.SaveChangesAsync();
                _logger.LogInformation("User {UserId} inserted successfully.", item.Id);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting user {UserId}.", item.Id);
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<Response> Update(User item)
        {
            var user = await _db.User.FindAsync(item.Id);
            if (user == null)
            {
                _logger.LogWarning("Update failed: User {UserId} not found.", item.Id);
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            }
            try
            {
                _db.User.Update(item);
                await _db.SaveChangesAsync();
                _logger.LogInformation("User {UserId} updated successfully.", item.Id);
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}.", item.Id);
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }
    }
}