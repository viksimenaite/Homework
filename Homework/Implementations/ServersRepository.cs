using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LiteDB.Async;

namespace Homework
{
    public class ServersRepository : IServersRepository
    {
        private readonly string dbName = @"Servers.db";
        private readonly string serversCollectionName = "servers";
        private static DatabaseManager dbManager;

        public ServersRepository()
        {
            LiteDatabaseAsync database = new LiteDatabaseAsync(dbName);
            dbManager = new DatabaseManager(database);
        }

        public async Task<List<ServerResponse>> GetAllServersFromDBAsync()
        { 
            List<ServerResponse> serverList = null;
            var serversCollection = dbManager.GetCollection<ServerResponse>(serversCollectionName);

            serverList = await dbManager.GetAllRecordsAsync<ServerResponse>(serversCollection);

            return serverList.OrderByDescending(s => s.Distance).ToList();
        }

        public async Task StoreAllServersIntoDBAsync(List<ServerResponse> serversList)
        {
            var serversCollection = dbManager.GetCollection<ServerResponse>(serversCollectionName);

            await dbManager.DeleteAllRecordsAsync<ServerResponse>(serversCollection);

            await dbManager.InsertRecordsAsync<ServerResponse>(serversList, serversCollection);
        }

    }
}
