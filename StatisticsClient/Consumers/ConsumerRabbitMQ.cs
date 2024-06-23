using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatisticsClient.Helpers;
using StatisticsClient.Repositories;
using StatisticsServer.Models;
using System.Text;
using System.Text.Json;
namespace StatisticsClient.Consumers
{
    public class ConsumerRabbitMQ : StatisticsServer.Rabbit.RabbitMQ, IConsumerRabbitMQ
    {
        private EventingBasicConsumer _consumer;
        private string _queueName;
        private MonogDBRepo _mongoDBRepo;
        private AlertHelper _alert;

        public ConsumerRabbitMQ(string _queueName)
        {
            this._queueName = _queueName;
            ChannelCreation();
            QueueDeclared(_queueName);
            _mongoDBRepo = new();
            _alert = new();
            EstablishConsumer();
        }
        public void EstablishConsumer()
        {
            string message = string.Empty;
            _consumer = new EventingBasicConsumer(channel);
            _consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received {message}");
                var currentObj = JsonSerializer.Deserialize<ServerStatistics>(message);
                var prevObj = await _mongoDBRepo.GetPreviousObject();
                await _mongoDBRepo.Insert(currentObj!);
                _alert.CheckThresholds(currentObj!, prevObj);
            };
        }

        public void ReceiveData()
        {
            channel.BasicConsume(queue: _queueName,
                      autoAck: true,
                      consumer: _consumer);
        }
    }
}
