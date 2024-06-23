using RabbitMQ.Client;

namespace StatisticsServer.Publishers
{
    public class PublisherRabbitMQ : Rabbit.RabbitMQ, IPublisherRabbitMQ
    {
        public PublisherRabbitMQ(string _queueName)
        {
            ChannelCreation();
            QueueDeclared(_queueName);
        }

        public void PublishData(string routingKey, byte[] body)
        {
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
        }

    }
}
