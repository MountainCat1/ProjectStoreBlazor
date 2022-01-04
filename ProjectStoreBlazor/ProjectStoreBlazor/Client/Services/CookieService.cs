using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Client.Services
{
    public class CookieService : ICookieService
    {
        private IJSRuntime js;

        public CookieService(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task WriteCookie(string name, string value)
        {
            await js.InvokeAsync<object>("WriteCookie.WriteCookie", name, value, DateTime.Now.AddMinutes(1));
        }

        public async Task<string> ReadCookie(string name)
        {
            return await js.InvokeAsync<string>("ReadCookie.ReadCookie", name);
        }
    }
}
