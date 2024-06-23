using MongoDB.Bson;

namespace StatisticsServer.Models
{
    public class ServerStatistics
    {

        public ObjectId Id { get; set; }
        public double MemoryUsage { get; set; }
        public double AvailableMemory { get; set; }
        public double CpuUsage { get; set; }
        public DateTime Timestamp { get; set; }


    }
}
