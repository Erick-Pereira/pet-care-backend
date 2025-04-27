namespace BLL.Interfaces
{
    public interface IHashService
    {
        Task<string> HashPasswordAsync(string password);

        Task<bool> VerifyPasswordAsync(string password, string hashedPassword);
    }
}