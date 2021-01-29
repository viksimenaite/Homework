using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Linq;

namespace Homework
{
    public class ServerManager
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private  ITesonetApi api;

        public ServerManager(ITesonetApi api)
        {
            this.api = api;
        }

        public async Task<List<ServerResponse>> GetAllRecordsAsync(TokensResponse token)
        {
            List<ServerResponse> recordList = null;

            HttpResponseMessage response = await api.GetServersAsync(token);
            if (response.IsSuccessStatusCode)
            {
                recordList = await response.Content.ReadAsAsync<List<ServerResponse>>();
                logger.Info("Successfully returned {0} records from API. Status code: {1}", recordList.Count, (int)response.StatusCode);
            }
            else
            {
                logger.Error("Failed to get servers from API. Status code: {0}", (int)response.StatusCode);
            }

            return recordList.OrderByDescending(s => s.Distance).ToList();
        }
    }
}
