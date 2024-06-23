namespace StatisticsClient.Consumers
{
    public interface IConsumerRabbitMQ
    {
        public void EstablishConsumer();
        public void ReceiveData();
    }
}
