using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server.Services
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto dto);
        public string GenerateJwt(LoginDto dto);
    }
}