using StatisticsServer.Models;

namespace StatisticsClient.Repositories
{
    public interface IStatisticsObjectRepo
    {
        public Task Insert(ServerStatistics obj);
        public Task<ServerStatistics> GetPreviousObject();
    }
}
