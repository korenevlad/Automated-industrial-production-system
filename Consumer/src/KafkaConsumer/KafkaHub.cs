using Microsoft.AspNetCore.SignalR;

namespace KafkaConsumer;
public class KafkaHub : Hub
{
    public async Task SendMessage(string topic, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", topic, message);
    }
}