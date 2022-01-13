using ProjectStoreBlazor.Client.Extensions;
using ProjectStoreBlazor.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Client.Services
{
    public class UserService : IUserService
    {
        private readonly ICookieService cookieService;
        private readonly HttpClient client;

        private string controllerRoute = "api/user";

        public UserService(ICookieService cookieService, HttpClient client)
        {
            this.cookieService = cookieService;
            this.client = client;
        }
        public async Task<UserDto> WhoAmI()
        {
            string authToken = await cookieService.ReadCookie("Authorization");

            UserDto userDto = await client.ReadJsonAsync<UserDto>($"{controllerRoute}", authToken);

            return userDto;
        }
    }
}
