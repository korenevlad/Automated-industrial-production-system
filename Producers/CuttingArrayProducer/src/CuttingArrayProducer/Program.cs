﻿using System.Text.Json;
using Confluent.Kafka;

namespace CuttingArrayProducer;
public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ProducerConfig { BootstrapServers = "kafka:9093" };
        var topic = "cutting_array_producer";
        double[] pressure = { 35, 0.5};
        double[] speed = { 4000, 50};
        
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            while (true)
            {
                var sensorData = new
                {
                    Time = DateTime.UtcNow,
                    Pressure = GenerateBoxMuller(pressure[0], pressure[1]),
                    Speed = GenerateBoxMuller(speed[0], speed[1]),
                };
                string message = JsonSerializer.Serialize(sensorData);
                try
                {
                    var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Параметры резки массива: {message}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Ошибка отправки параметров резки массива: {e.Error.Reason}");
                }

                await Task.Delay(1000);
            }
        }
    }
    
    private static double GenerateBoxMuller(double current, double deviation)
    {
        var random = new Random();
        var u1 = 1.0 - random.NextDouble();
        var u2 = 1.0 - random.NextDouble();
        var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        var temperatureChange = randStdNormal * deviation;
        return current + temperatureChange;
    }
}