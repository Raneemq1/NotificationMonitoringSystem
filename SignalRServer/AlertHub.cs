using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    public class AlertHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Console.Out.WriteLineAsync(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }
}
