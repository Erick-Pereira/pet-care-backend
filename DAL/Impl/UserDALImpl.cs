using Commons.Responses;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Impl
{
    internal class UserDALImpl : IUserDAL
    {
        private readonly DataBaseDbContext _db;
        private readonly ILogger<UserDALImpl> _logger;

        public UserDALImpl(DataBaseDbContext db, ILogger<UserDALImpl> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Response> Delete(Guid id)
        {
            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponseNotFoundId();
            }
            try
            {
                _db.User.Remove(user);
                await _db.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        public async Task<SingleResponse<User>> Get(Guid id)
        {
            try
            {
                var user = _db.User.AsNoTracking().FirstOrDefault(x => x.Id == id);
                return ResponseFactory.CreateInstance().CreateSuccessSingleResponse(user);
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedSingleResponseNotFoundItem<User>(ex);
            }
        }

        public async Task<DataResponse<User>> Get(int skip, int take)
        {
            var users = await _db.User
                .OrderBy(u => u.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new DataResponse<User>
            {
                Success = true,
                Data = users
            };
        }

        public async Task<DataResponse<User>> Get(int skip, int take, string createdBy = null, bool? active = null)
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

            return new DataResponse<User>
            {
                Success = true,
                Data = users
            };
        }

        public async Task<Response> Insert(User item)
        {
            try
            {
                _db.User.Add(item);
                await _db.SaveChangesAsync();
                _logger.LogInformation("User {UserId} inserted successfully.", item.Id);
                return new Response { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting user {UserId}.", item.Id);
                return new Response { Success = false, Message = ex.Message };
            }
        }

        public Task<Response> Update(User Item)
        {
            throw new NotImplementedException();
        }
    }
}