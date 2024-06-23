using MongoDB.Bson;
using StatisticsServer.Models;
using System.Diagnostics;
namespace StatisticsServer.Helpers
{
    public class ServerStatisticsCollector
    {

        public ServerStatistics GetStatisticsObject()
        {
            double memoryUsage = GetMemoryUsage();
            double availableMemory = GetAvailableMemory();
            double cpuUsage = GetCpuUsage();
            DateTime timeStamp = DateTime.Now;
            ServerStatistics serverStat = new ServerStatistics { Id = ObjectId.GenerateNewId(), MemoryUsage = memoryUsage, AvailableMemory = availableMemory, CpuUsage = cpuUsage, Timestamp = timeStamp };
            return serverStat;
        }
        private double GetMemoryUsage()
        {
            using var process = Process.GetCurrentProcess();
            return process.WorkingSet64 / (1024 * 1024);
        }
        private double GetAvailableMemory()
        {
            using var pc = new PerformanceCounter("Memory", "Available MBytes");
            return pc.NextValue();
        }
        private double GetCpuUsage()
        {
            using var pc = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            pc.NextValue();
            Thread.Sleep(1000);
            return pc.NextValue();
        }

    }
}
