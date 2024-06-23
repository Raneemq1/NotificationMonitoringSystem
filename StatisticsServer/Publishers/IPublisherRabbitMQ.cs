namespace StatisticsServer.Publishers
{
    public interface IPublisherRabbitMQ
    {
        public void PublishData(string routingKey, byte[] body);
    }
}
