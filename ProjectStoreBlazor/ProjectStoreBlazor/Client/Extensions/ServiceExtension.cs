using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectStoreBlazor.Client.Extensions
{

    public static class ServiceExtensions
    {
        public static async Task<T> ReadJsonAsync<T>(this HttpClient httpClient, string url, string token) where T : class
        {
            // create request object
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            // add authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // send request
            var httpResponse = await httpClient.SendAsync(request);

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            // convert http response data to UsersResponse object
            return await httpResponse.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string url, string token)
        {
            // create request object
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            // add authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // send request
            return await httpClient.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> PostJsonAsync<T>(this HttpClient httpClient, string url, T obj,  string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            // set request body
            var postBody = obj;
            request.Content = new StringContent(JsonSerializer.Serialize(postBody), Encoding.UTF8, "application/json");

            // add authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


            // send request
            return await httpClient.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> PutJsonAsync<T>(this HttpClient httpClient, string url, T obj, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            // set request body
            var postBody = obj;
            request.Content = new StringContent(JsonSerializer.Serialize(postBody), Encoding.UTF8, "application/json");

            // add authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


            // send request
            return await httpClient.SendAsync(request);
        }
    }
}
