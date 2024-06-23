using Microsoft.Extensions.Configuration;
using StatisticsServer.Helpers;
using StatisticsServer.Publishers;
using System.Text.Json;

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
int intervalTime = int.Parse(config["ServerStatisticsConfig:SamplingIntervalSeconds"])*1000;
string serverIdentifier ="ServerStatistics:<"+config["ServerStatisticsConfig:ServerIdentifier"]+">";
PublisherRabbitMQ publisher = new(serverIdentifier);
var timer=new Timer(SendReport,null,0,intervalTime);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
void SendReport(object state)
{
    ServerStatisticsCollector serverCollector = new();
    var serverStatistics = serverCollector.GetStatisticsObject();
    var msg = JsonSerializer.Serialize(serverStatistics);
    var body = System.Text.Encoding.UTF8.GetBytes(msg);
    publisher.PublishData(serverIdentifier, body);
    Console.WriteLine(msg);
    
}

