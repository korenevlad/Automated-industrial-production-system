using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;

namespace KafkaConsumer;
public class KafkaConsumerService : BackgroundService
{
    private const string MixingComponentsProducerTopic = "mixing_components_producer";
    private const string MoldingAndInitialExposureProducerTopic = "molding_and_initial_exposure_producer";
    private const string CuttingArrayProducerTopic = "cutting_array_producer";
    private const string AutoclavingProducerTopic = "autoclaving_producer";
    private const string GroupId = "backend-group";
    private const string BootstrapServers = "kafka:9093";
    private readonly ConsumerConfig _consumerConfig = new()
    {
        BootstrapServers = BootstrapServers,
        GroupId = GroupId,
        AutoOffsetReset = AutoOffsetReset.Earliest
    };
    private readonly IHubContext<KafkaHub> _hubContext;
    
    public KafkaConsumerService(IHubContext<KafkaHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.WhenAll(
            Task.Run(() => MixingComponentsProducerTopicConsumeMessages(stoppingToken), stoppingToken),
            Task.Run(() => MoldingAndInitialExposureProducerConsumeMessages(stoppingToken), stoppingToken),
            Task.Run(() => CuttingArrayProducerConsumeMessages(stoppingToken), stoppingToken),
            Task.Run(() => AutoclavingProducerConsumeMessages(stoppingToken), stoppingToken)
        );
    }
    
    private async Task MixingComponentsProducerTopicConsumeMessages(CancellationToken stoppingToken)
    {
        await ConsumeMessages(stoppingToken, MixingComponentsProducerTopic, 
            "Полученные параметры смешивания компонентов", 
            "Ошибка получения параметров смешивания компонентов" );
    }
    private async Task CuttingArrayProducerConsumeMessages(CancellationToken stoppingToken)
    {
        await ConsumeMessages(stoppingToken, CuttingArrayProducerTopic, 
            "Полученные параметры резки массива", 
            "Ошибка получения параметров резки массива" );
    }
    
    private async Task MoldingAndInitialExposureProducerConsumeMessages(CancellationToken stoppingToken)
    {
        await ConsumeMessages(stoppingToken, MoldingAndInitialExposureProducerTopic, 
            "Полученные параметры формования и первичной выдержки", 
            "Ошибка получения параметров формования и первичной выдержки" );
    }
    
    private async Task AutoclavingProducerConsumeMessages(CancellationToken stoppingToken)
    {
        await ConsumeMessages(stoppingToken, AutoclavingProducerTopic, 
            "Полученные параметры автоклавирования и окончательной обработки", 
            "Ошибка получения параметров автоклавирования и окончательной обработки" );
    }
    
    private async Task ConsumeMessages(CancellationToken stoppingToken, string topic, string successMessage, string errorMessage)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        consumer.Subscribe(topic);
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    Console.WriteLine($"{successMessage}: {consumeResult.Value}");
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", topic, consumeResult.Value);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"{errorMessage}: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}