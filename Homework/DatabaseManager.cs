using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LiteDB.Async;

namespace Homework
{
    public class DatabaseManager
    {
        private readonly LiteDatabaseAsync database;
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DatabaseManager(LiteDatabaseAsync database)
        {
            this.database = database;
        }

        public LiteCollectionAsync<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }

        public async Task DeleteAllRecordsAsync<T>(LiteCollectionAsync<T> collection)
        {
            int noOfDeletedEntries = await collection.DeleteAllAsync();
            logger.Info("Cache was purged. {0} entries deleted", noOfDeletedEntries);
        }

        public async Task InsertRecordsAsync<T>(List<T> recordsList, LiteCollectionAsync<T> collection)
        {
            bool insertSuccessful = true;
            foreach (var item in recordsList)
            {
                if (!(await collection.UpsertAsync(item)))
                {
                    insertSuccessful = false;
                }
            }

            if (insertSuccessful)
            {
                logger.Info("Records were inserted successfully");
            }
            else
            {
                logger.Info("An error occurred while inserting records");
            }
        }

        public async Task<List<T>> GetAllRecordsAsync<T>(LiteCollectionAsync<T> collection)
        {
            List<T> recordsList = await collection.Query().ToListAsync();
            logger.Info("Retrieved {0} objects from database", recordsList.Count);

            return recordsList;
        }

    }
}
