namespace StatisticsServer.Rabbit
{
    public interface IRabbitMQ
    {
        public void ChannelCreation();
        public void QueueDeclared(string queueName);
    }
}
