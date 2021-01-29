using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Homework
{
    public class TesonetApi : ITesonetApi
    {
        private readonly HttpClient httpClient;
        private readonly string baseURL = "https://playground.tesonet.lt";

        public TesonetApi()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseURL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetServersAsync(TokensResponse token)
        { 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

            HttpResponseMessage response = await httpClient.GetAsync(baseURL + "/v1/servers");

            return response;
        }

        public async Task<HttpResponseMessage> PostTokensAsync(CredentialsPayload credentials)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("/v1/tokens", credentials);

            return response;
        }
    }
}
