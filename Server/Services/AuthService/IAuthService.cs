using Shared;

namespace Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExixts(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
    }
}
