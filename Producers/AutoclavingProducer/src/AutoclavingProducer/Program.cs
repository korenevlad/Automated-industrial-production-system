﻿using System.Text.Json;
using Confluent.Kafka;

namespace AutoclavingProducer;
public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ProducerConfig { BootstrapServers = "kafka:9093" };
        string topic = "autoclaving_producer";
        double[] temperature = { 200, 0.5};
        double[] pressure = { 1.22, 0.005};
        
        var timerDuration = TimeSpan.FromMinutes(720);
        var startTime = DateTime.UtcNow;

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            while (true)
            {
                var elapsedTime = DateTime.UtcNow - startTime;
                var remainingTime = timerDuration - elapsedTime;
                var sensorData = new
                {
                    Time = DateTime.UtcNow,
                    Temperature = GenerateBoxMuller(temperature[0], temperature[1], 199.5, 200.5, 0.1),
                    Pressure = GenerateBoxMuller(pressure[0], pressure[1], 1.21995, 1.22005, 0.1),
                    Remaining_process_time = remainingTime
                };
                string message = JsonSerializer.Serialize(sensorData);
                try
                {
                    var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Параметры автоклавирования: {message}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Ошибка отправки параметров автоклавирования: {e.Error.Reason}");
                }

                await Task.Delay(1000);
            }
        }
    }
    
    private static double GenerateBoxMuller(double current, double deviation, double errorMin, double errorMax, double errorProbability)
    {
        var random = new Random();
        var u1 = 1.0 - random.NextDouble();
        var u2 = 1.0 - random.NextDouble();
        var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        var value = current + (randStdNormal * deviation);

        if (random.NextDouble() < errorProbability)
        {
            if (random.NextDouble() < 0.5)
                return errorMin - (random.NextDouble() * deviation);
            else
                return errorMax + (random.NextDouble() * deviation);
        }
        return value;
    }
}