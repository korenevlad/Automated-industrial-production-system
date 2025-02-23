using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace KafkaConsume;
public class KafkaConsumerService : BackgroundService
{
    private const string Topic = "mixing_components_producer";
    private const string GroupId = "backend-group";
    private const string BootstrapServers = "kafka:9093";

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeMessages(stoppingToken), stoppingToken);
    }

    private void ConsumeMessages(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(Topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    Console.WriteLine($"Получено сообщение: {consumeResult.Value}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Ошибка при получении сообщения: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}