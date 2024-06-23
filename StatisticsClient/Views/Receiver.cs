using Microsoft.Extensions.Configuration;
using StatisticsClient.Consumers;

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
string serverIdentifier = "ServerStatistics:<" + config["ServerStatisticsConfig:ServerIdentifier"] + ">";
var consumer=new ConsumerRabbitMQ(serverIdentifier);
consumer.ReceiveData();
Console.ReadLine();