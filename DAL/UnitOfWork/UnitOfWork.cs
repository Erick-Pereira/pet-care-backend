using Commons.Responses;
using DAL.Impl;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataBaseDbContext _dbContext;
        private readonly ILogger<UserDALImpl> _logger;
        private IUserDAL _userRepository;
        private bool _disposed = false;

        public UnitOfWork(DataBaseDbContext context, ILogger<UserDALImpl> logger)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IUserDAL UserRepository
        {
            get
            {
                return _userRepository ??= new UserDALImpl(_dbContext, _logger);
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Response> Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse(ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}