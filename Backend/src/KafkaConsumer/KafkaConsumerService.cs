using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

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
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.WhenAll(
            Task.Run(() => MixingComponentsProducerTopicConsumeMessages(stoppingToken), stoppingToken),
            Task.Run(() => MoldingAndInitialExposureProducerConsumeMessages(stoppingToken), stoppingToken),
            Task.Run(() => CuttingArrayProducerConsumeMessages(stoppingToken), stoppingToken),
            Task.Run(() => AutoclavingProducerConsumeMessages(stoppingToken), stoppingToken)
        );
    }
    
    private void MixingComponentsProducerTopicConsumeMessages(CancellationToken stoppingToken)
    {
        ConsumeMessages(stoppingToken, MixingComponentsProducerTopic, 
            "Полученные параметры смешивания компонентов", 
            "Ошибка получения параметров смешивания компонентов" );
    }
    private void CuttingArrayProducerConsumeMessages(CancellationToken stoppingToken)
    {
        ConsumeMessages(stoppingToken, CuttingArrayProducerTopic, 
            "Полученные параметры резки массива", 
            "Ошибка получения параметров резки массива" );
    }
    
    private void MoldingAndInitialExposureProducerConsumeMessages(CancellationToken stoppingToken)
    {
        ConsumeMessages(stoppingToken, MoldingAndInitialExposureProducerTopic, 
            "Полученные параметры формования и первичной выдержки", 
            "Ошибка получения параметров формования и первичной выдержки" );
    }
    
    private void AutoclavingProducerConsumeMessages(CancellationToken stoppingToken)
    {
        ConsumeMessages(stoppingToken, AutoclavingProducerTopic, 
            "Полученные параметры автоклавирования и окончательной обработки", 
            "Ошибка получения параметров автоклавирования и окончательной обработки" );
    }
    
    private void ConsumeMessages(CancellationToken stoppingToken, string topic, string successMessage, string errorMessage)
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