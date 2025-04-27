using BLL.Interfaces;

namespace BLL.Impl
{
    public class HashServiceImpl : IHashService
    {
        public Task<string> HashPasswordAsync(string password)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
        }

        public Task<bool> VerifyPasswordAsync(string password, string hashedPassword)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, hashedPassword));
        }
    }
}