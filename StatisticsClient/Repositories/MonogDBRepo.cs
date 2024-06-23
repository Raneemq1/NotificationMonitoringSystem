using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StatisticsServer.Models;

namespace StatisticsClient.Repositories
{
    public class MonogDBRepo : IStatisticsObjectRepo
    {

        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<ServerStatistics> _collection;

        public MonogDBRepo()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string connectionString = config["MongoDB:ConnectionString"];
            try
            {
                _client = new MongoClient(connectionString);
                _database = _client.GetDatabase("NotificationSystem");
                _collection = _database.GetCollection<ServerStatistics>("StatisticsServerObj");
            }
            catch { throw; }
        }

        public async Task<ServerStatistics> GetPreviousObject()
        {
            ServerStatistics obj;
            var doc = await _collection.Find(_ => true).SortByDescending(doc => doc.Timestamp).FirstOrDefaultAsync();
            if (doc is null) obj = new ServerStatistics { MemoryUsage = 0, AvailableMemory = 0, CpuUsage = 0 };
            obj = new ServerStatistics { MemoryUsage = doc.MemoryUsage, AvailableMemory = doc.AvailableMemory, CpuUsage = doc.CpuUsage, Timestamp = doc.Timestamp };
            return obj;
        }

        public async Task Insert(ServerStatistics obj) => await _collection.InsertOneAsync(obj);


    }
}
