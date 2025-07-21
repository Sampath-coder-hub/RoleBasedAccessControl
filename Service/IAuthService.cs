using Assignment.Models;
namespace Assignment.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        void CreatePasswordHash(string password, out byte[] hash, out byte[] salt);
        bool VerifyPassword(string password, byte[] hash, byte[] salt);
    }
}
