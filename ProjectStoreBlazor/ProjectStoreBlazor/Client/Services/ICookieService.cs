using System.Threading.Tasks;

namespace ProjectStoreBlazor.Client.Services
{
    public interface ICookieService
    {
        Task<string> ReadCookie(string name);
        Task WriteCookie(string name, string value);
    }
}
