using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public interface ITesonetApi
    {
        Task<HttpResponseMessage> PostTokensAsync(CredentialsPayload credentialsPayload);
        Task<HttpResponseMessage> GetServersAsync(TokensResponse token);
    }
}
