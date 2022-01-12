using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server.Services
{
    public interface IUserService
    {
        string GenerateJwt(LoginDto dto);
        UserDto GetUserFromJWTToken(int userId);
        void RegisterUser(RegisterUserDto dto);
    }
}