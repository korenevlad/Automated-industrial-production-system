using System.Text.Encodings.Web;
using System.Text.Json;
using Confluent.Kafka;
using KafkaConsumer.DataAccess.Repository;
using KafkaConsumer.Models;
using KafkaConsumer.Models.Mappers;
using KafkaConsumer.Models.Serializes;
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
    
    private bool _startedAutoclavingProcess;
    private Autoclaving_process _autoclavingProcess;
    
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

        _startedAutoclavingProcess = false;
        _autoclavingProcess = new Autoclaving_process();
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
                            var messageMixing = JsonSerializer.Deserialize<MixingComponentsProducerMessage>(consumeResult.Value);
                            SaveMessageFromMixingInDatabase(messageMixing);
                            var messageMixingToSerialize = MixingComponentsProducerMessageMapper.Map(messageMixing);
                            await _hubContext.Clients.All.SendAsync("ReceiveMessage", topic, JsonSerializer.Serialize(
                                messageMixingToSerialize,
                                new JsonSerializerOptions 
                                { 
                                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
                                    WriteIndented = true 
                                }));
                            break;
                        case MoldingAndInitialExposureProducerTopic:
                            var messageMolding = JsonSerializer.Deserialize<MoldingProducerMessage>(consumeResult.Value);
                            SaveMessageFromMoldingInDatabase(messageMolding);
                            var messageMoldingToSerialize = MoldingProducerMessageMapper.Map(messageMolding);
                            await _hubContext.Clients.All.SendAsync("ReceiveMessage", topic, JsonSerializer.Serialize(
                                messageMoldingToSerialize,
                                new JsonSerializerOptions 
                                { 
                                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
                                    WriteIndented = true 
                                }));
                            break;
                        case CuttingArrayProducerTopic:
                            var messageCutting = JsonSerializer.Deserialize<СuttingArrayProducerMessage>(consumeResult.Value);
                            SaveMessageFromСuttingArrayInDatabase(messageCutting);
                            var messageCuttingToSerialize = СuttingArrayProducerMessageMapper.Map(messageCutting);
                            await _hubContext.Clients.All.SendAsync("ReceiveMessage", topic, JsonSerializer.Serialize(
                                messageCuttingToSerialize,
                                new JsonSerializerOptions 
                                { 
                                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
                                    WriteIndented = true 
                                }));
                            break;
                        case AutoclavingProducerTopic:
                            var messageAutoclaving = JsonSerializer.Deserialize<AutoclavingProducerMessage>(consumeResult.Value);
                            SaveMessageFromAutoclavingInDatabase(messageAutoclaving);
                            var messageAutoclavingToSerialize = AutoclavingProducerMessageMapper.Map(messageAutoclaving);
                            await _hubContext.Clients.All.SendAsync("ReceiveMessage", topic, JsonSerializer.Serialize(
                                messageAutoclavingToSerialize,
                                new JsonSerializerOptions 
                                { 
                                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
                                    WriteIndented = true 
                                }));
                            break;
                    }
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
    private void SaveMessageFromMixingInDatabase(MixingComponentsProducerMessage message)
    {
        try
        {
            if (!_startedTechnologicalProcess)
            {
                _technologicalProcess.date_start = message.Time;
                _unitOfWork.TechnologicalProcessRepository.Add(_technologicalProcess);
                _unitOfWork.Save();
                _startedTechnologicalProcess = true;
            }
            if (!_startedMixingProcess)
            {
                _mixingProcess.date_start = message.Time;
                _mixingProcess.Technological_process_of_mixing_process = _technologicalProcess;
                _unitOfWork.MixingProcessRepository.Add(_mixingProcess);
                _unitOfWork.Save();
                _startedMixingProcess = true;
            }
            var messageDbo = new Parameters_mixing_process()
            {
                Mixing_process_of_parameters = _mixingProcess,
                init_time = message.Time,
                temperature_mixture = message.Temperature_mixture,
                temperature_mixture_is_normal = (message.Temperature_mixture > 46 || message.Temperature_mixture < 44)? false : true,
                mixing_speed = message.Mixing_speed,
                mixing_speed_is_normal = (message.Mixing_speed > 51 || message.Mixing_speed < 49) ? false : true,
                remaining_process_time = message.Remaining_process_time
            };
            _mixingProcess.date_end = message.Time;
            _unitOfWork.MixingProcessRepository.Update(_mixingProcess);
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
    private void SaveMessageFromMoldingInDatabase(MoldingProducerMessage message)
    {
        try
        {
            if (!_startedMoldingProcess)
            {
                _moldingProcess.date_start = message.Time;
                _moldingProcess.Technological_process_of_molding_process = _technologicalProcess;
                _unitOfWork.MoldingAndInitialExposureProcessRepository.Add(_moldingProcess);
                _unitOfWork.Save();
                _startedMoldingProcess = true;
            }
            var messageDto = new Parameters_molding_and_initial_exposure_process()
            {
                Molding_and_initial_exposure_process_of_parameters = _moldingProcess,
                init_time = message.Time,
                temperature = message.Temperature,
                temperature_is_normal = (message.Temperature > 36 || message.Temperature < 34) ? false : true,
                remaining_process_time = message.Remaining_process_time
            };
            _moldingProcess.date_end = message.Time;
            _unitOfWork.MoldingAndInitialExposureProcessRepository.Update(_moldingProcess);
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
    private void SaveMessageFromСuttingArrayInDatabase(СuttingArrayProducerMessage message)
    {
        try
        {
            if (!_startedCuttingArrayProcess)
            {
                _cuttingArrayProcess.date_start = message.Time;
                _cuttingArrayProcess.Technological_process_of_mixing_process = _technologicalProcess;
                _unitOfWork.CuttingArrayProcessRepository.Add(_cuttingArrayProcess);
                _unitOfWork.Save();
                _startedCuttingArrayProcess = true;
            }
            var messageDto = new Parameters_cutting_array_process()
            {
                cutting_array_process_of_parameters = _cuttingArrayProcess,
                init_time = message.Time,
                pressure = message.Pressure,
                pressure_is_normal = (message.Pressure > 36 || message.Pressure < 34) ? false : true,
                speed = message.Speed,
                speed_is_normal = (message.Speed > 4100 || message.Speed < 3900) ? false : true
            };
            _cuttingArrayProcess.date_end = message.Time;
            _unitOfWork.CuttingArrayProcessRepository.Update(_cuttingArrayProcess);
            _unitOfWork.ParametersCuttingArrayProcessRepository.Add(messageDto);
            _unitOfWork.Save();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения данных резки массива! {ex.Message}");
            throw;
        }
    }
    
    // Сохранение автоклавирования
    private void SaveMessageFromAutoclavingInDatabase(AutoclavingProducerMessage message)
    {
        try
        {
            if (!_startedAutoclavingProcess)
            {
                _autoclavingProcess.date_start = message.Time;
                _autoclavingProcess.Technological_process_of_mixing_process = _technologicalProcess;
                _unitOfWork.AutoclavingProcessRepository.Add(_autoclavingProcess);
                _unitOfWork.Save();
                _startedAutoclavingProcess = true;
            }
            var messageDto = new Parameters_autoclaving_process()
            {
                autoclaving_process_of_parameters = _autoclavingProcess,
                init_time = message.Time,
                temperature = message.Temperature,
                temperature_is_normal = (message.Temperature < 199 || message.Temperature > 201) ? false : true,
                pressure = message.Pressure,
                pressure_is_normal = (message.Pressure < 1.21 || message.Pressure >= 1.23) ? false : true,
                remaining_process_time = message.Remaining_process_time
            };
            _autoclavingProcess.date_end = message.Time;
            _unitOfWork.AutoclavingProcessRepository.Update(_autoclavingProcess);
            _technologicalProcess.date_end = message.Time;
            _unitOfWork.TechnologicalProcessRepository.Update(_technologicalProcess);
            _unitOfWork.ParametersAutoclavingProcessRepository.Add(messageDto);
            _unitOfWork.Save();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения данных автоклавирования! {ex.Message}");
            throw;
        }
    } 
}