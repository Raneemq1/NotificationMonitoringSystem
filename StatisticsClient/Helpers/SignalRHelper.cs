using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;


namespace StatisticsClient.Helpers
{
    public class SignalRHelper
    {
        private HubConnection _connection;

        public SignalRHelper()
        {
            BuildConnectionWithHub();
            EventHandlerForReceivingMessages();
            StartConnection();
        }
        private void BuildConnectionWithHub()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string url = config["SignalRConfig:SignalRUrl"];
            _connection = new HubConnectionBuilder()
        .WithUrl($"{url}/AlertHub")
        .Build();
        }

        private async void StartConnection() => await _connection.StartAsync();


        public async void StopConnection() => await _connection.StopAsync();

        public async void SendMessage(string message) => await _connection.InvokeAsync("SendMessage", message);

        public async void EventHandlerForReceivingMessages()
        {
            _connection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine($"{message}");
            });
        }
    }
}
