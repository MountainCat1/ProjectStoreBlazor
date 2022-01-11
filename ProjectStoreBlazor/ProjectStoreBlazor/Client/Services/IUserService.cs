using ProjectStoreBlazor.Shared.Models;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Client.Services
{
    public interface IUserService
    {
        public Task<UserDto> WhoAmI();
    }
}
