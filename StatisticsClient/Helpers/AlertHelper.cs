using Microsoft.Extensions.Configuration;
using StatisticsServer.Models;

namespace StatisticsClient.Helpers
{
    public class AlertHelper
    {
        private double _cpuAnaomlyPercentage;
        private double _memoryAnaomlyPercentage;
        private double _memoryThresoldPercentage;
        private double _cpuThresoldPercentage;

        public AlertHelper()
        {
            var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
            _cpuAnaomlyPercentage = double.Parse(config["AnomalyDetectionConfig:CpuUsageAnomalyThresholdPercentage"]);
            _memoryAnaomlyPercentage = double.Parse(config["AnomalyDetectionConfig:MemoryUsageAnomalyThresholdPercentage"]);
            _memoryThresoldPercentage = double.Parse(config["AnomalyDetectionConfig:MemoryUsageThresholdPercentage"]);
            _cpuThresoldPercentage = double.Parse(config["AnomalyDetectionConfig:CpuUsageThresholdPercentage"]);
        }
        public void CheckThresholds(ServerStatistics currentObj, ServerStatistics prevObj)
        {
            var signalRHelper = new SignalRHelper();
            if (MemoryUsageAnomalyAlert(currentObj, prevObj) || CpuUsageAnomalyAlert(currentObj, prevObj))
                signalRHelper.SendMessage("Anomaly Alert");

            if (MemoryHighUsageAlert(currentObj, prevObj) || CpuHighUsageAlert(currentObj, prevObj))
                signalRHelper.SendMessage("High Usage Alert");

        }

        public bool MemoryUsageAnomalyAlert(ServerStatistics currentObj, ServerStatistics prevObj)
        {
            if (currentObj.MemoryUsage > prevObj.MemoryUsage * (1 + _memoryAnaomlyPercentage))
                return true;
            return false;

        }
        public bool CpuUsageAnomalyAlert(ServerStatistics currentObj, ServerStatistics prevObj)
        {
            if (currentObj.CpuUsage > prevObj.CpuUsage * (1 + _cpuAnaomlyPercentage))
                return true;
            return false;
        }
        public bool MemoryHighUsageAlert(ServerStatistics currentObj, ServerStatistics prevObj)
        {
            if (currentObj.MemoryUsage / (currentObj.MemoryUsage + currentObj.AvailableMemory) > _memoryThresoldPercentage)
                return true;
            return false;
        }
        public bool CpuHighUsageAlert(ServerStatistics currentObj, ServerStatistics prevObj)
        {
            if (currentObj.CpuUsage > _cpuThresoldPercentage)
                return true;
            return false;
        }
    }
}
