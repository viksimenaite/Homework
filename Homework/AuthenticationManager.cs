using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Homework
{
    public class AuthenticationManager
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private ITesonetApi api;

        public AuthenticationManager(ITesonetApi api)
        {
            this.api = api;
        }

        public async Task<TokensResponse> GetTokenAsync(CredentialsPayload credentials)
        {
            TokensResponse token = null;
            HttpResponseMessage response = await api.PostTokensAsync(credentials);

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsAsync<TokensResponse>();
                logger.Info("Authentication successful. Status code: {0}", (int)response.StatusCode);
            }
            else
            {
                logger.Error("Authentication is not successful. Status code: {0}", (int)response.StatusCode);
            }

            return token;
        }
    }
}
