using System.Text.Json;
using Confluent.Kafka;
using KafkaConsumer.DataAccess.Repository;
using KafkaConsumer.Models;
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
    private readonly IUnitOfWork _unitOfWork;
    
    private bool _startedTechnologicalProcess;
    private Technological_process _technologicalProcess;
    
    private bool _startedMixingProcess;
    private Mixing_process _mixingProcess;
    
    private bool _startedMoldingProcess;
    private Molding_and_initial_exposure_process _moldingProcess;
    
    private bool _startedCuttingArrayProcess;
    private Cutting_array_process _cuttingArrayProcess;
    
    public KafkaConsumerService(IHubContext<KafkaHub> hubContext, IUnitOfWork unitOfWork)
    {
        _hubContext = hubContext;
        _unitOfWork = unitOfWork;
        
        _startedTechnologicalProcess = false;
        _technologicalProcess = new Technological_process();
        
        _startedMixingProcess = false;
        _mixingProcess = new Mixing_process();

        _startedMoldingProcess = false;
        _moldingProcess = new Molding_and_initial_exposure_process();

        _startedCuttingArrayProcess = false;
        _cuttingArrayProcess = new Cutting_array_process();
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
                    switch (topic)
                    {
                        case MixingComponentsProducerTopic:
                            SaveMessageFromMixingInDatabase(consumeResult);
                            break;
                        case MoldingAndInitialExposureProducerTopic:
                            SaveMessageFromMoldingInDatabase(consumeResult);
                            break;
                        case CuttingArrayProducerTopic:
                            SaveMessageFromСuttingArrayInDatabase(consumeResult);
                            break;
                    }
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
    
    // Сохранение смешивания
    private void SaveMessageFromMixingInDatabase(ConsumeResult<Ignore,string> consumeResult)
    {
        try
        {
            var mesasge = JsonSerializer.Deserialize<MixingComponentsProducerMessage>(consumeResult.Value);
            if (!_startedTechnologicalProcess)
            {
                _unitOfWork.TechnologicalProcessRepository.Add(_technologicalProcess);
                _unitOfWork.Save();
                _startedTechnologicalProcess = true;
            }
            if (!_startedMixingProcess)
            {
                _mixingProcess.Technological_process_of_mixing_process = _technologicalProcess;
                _unitOfWork.MixingProcessRepository.Add(_mixingProcess);
                _unitOfWork.Save();
                _startedMixingProcess = true;
            }
            //TODO: ПРОСТАВИТЬ БУЛЫ
            var messageDbo = new Parameters_mixing_process()
            {
                Mixing_process_of_parameters = _mixingProcess,
                init_time = mesasge.Time,
                temperature_mixture = mesasge.Temperature_mixture,
                temperature_mixture_is_normal = true,
                mixing_speed = mesasge.Mixing_speed,
                mixing_speed_is_normal = true,
                remaining_process_time = mesasge.Remaining_process_time
            };
            _unitOfWork.ParametersMixingProcessRepository.Add(messageDbo);
            _unitOfWork.Save();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения данных смешивания компонентов! {ex.Message}");
            throw;
        }
    }

    // Сохранение формования
    private void SaveMessageFromMoldingInDatabase(ConsumeResult<Ignore, string> consumeResult)
    {
        try
        {
            var mesasge = JsonSerializer.Deserialize<MoldingProducerMessage>(consumeResult.Value);
            if (!_startedMoldingProcess)
            {
                _moldingProcess.Technological_process_of_molding_process = _technologicalProcess;
                _unitOfWork.MoldingAndInitialExposureProcessRepository.Add(_moldingProcess);
                _unitOfWork.Save();
                _startedMoldingProcess = true;
            }
            //TODO: ПРОСТАВИТЬ БУЛЫ
            var messageDto = new Parameters_molding_and_initial_exposure_process()
            {
                Molding_and_initial_exposure_process_of_parameters = _moldingProcess,
                init_time = mesasge.Time,
                temperature = mesasge.Temperature,
                temperature_is_normal = true,
                remaining_process_time = mesasge.Remaining_process_time
            };
            _unitOfWork.ParametersMoldingAndInitialExposureProcessRepository.Add(messageDto);
            _unitOfWork.Save();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения данных формования и первичной выдержки! {ex.Message}");
            throw;
        }
    }
    
    // Сохранение резки массива
    private void SaveMessageFromСuttingArrayInDatabase(ConsumeResult<Ignore, string> consumeResult)
    {
        try
        {
            var mesasge = JsonSerializer.Deserialize<СuttingArrayProducerMessage>(consumeResult.Value);
            if (!_startedCuttingArrayProcess)
            {
                _cuttingArrayProcess.Technological_process_of_mixing_process = _technologicalProcess;
                _unitOfWork.CuttingArrayProcessRepository.Add(_cuttingArrayProcess);
                _unitOfWork.Save();
                _startedCuttingArrayProcess = true;
            }
            //TODO: ПРОСТАВИТЬ БУЛЫ
            var messageDto = new Parameters_cutting_array_process()
            {
                cutting_array_process_of_parameters = _cuttingArrayProcess,
                init_time = mesasge.Time,
                pressure = mesasge.Pressure,
                pressure_is_normal = true,
                speed = mesasge.Speed,
                speed_is_normal = true
            };
            _unitOfWork.ParametersCuttingArrayProcess.Add(messageDto);
            _unitOfWork.Save();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения данных резки массива! {ex.Message}");
            throw;
        }
    }
}