using RabbitMQ.Client;

namespace StatisticsServer.Rabbit
{
    public class RabbitMQ : IRabbitMQ, IDisposable
    {
        public IModel channel { set; get; }
        private IConnection _connection;
        public void ChannelCreation()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            channel = _connection.CreateModel();

        }

        public void Dispose()
        {
            _connection?.Dispose();
            channel?.Dispose();

        }
        public void QueueDeclared(string queueName)
        {
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }
    }
}
