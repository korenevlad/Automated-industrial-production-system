using Microsoft.AspNetCore.SignalR;

namespace KafkaConsumer;
public class KafkaHub : Hub
{
    public async Task SendMessagw(string topic, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", topic, message);
    }
}