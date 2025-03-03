using System.Text.Json;
using Confluent.Kafka;

namespace MixingComponentsProducer;
public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ProducerConfig { BootstrapServers = "kafka:9093" };
        var topic = "mixing_components_producer";
        double[] temperature_mixture = { 45, 0.5};
        double[] mixing_speed = { 50, 0.5};
        
        var timerDuration = TimeSpan.FromMinutes(5);
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
                    Temperature_mixture = GenerateBoxMuller(temperature_mixture[0], temperature_mixture[1]),
                    Mixing_speed = GenerateBoxMuller(mixing_speed[0], mixing_speed[1]),
                    Remaining_process_time = remainingTime
                };
                string message = JsonSerializer.Serialize(sensorData);
                try
                {
                    var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Параметры смешивания компонентов: {message}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Ошибка отправки параметров смешивания компонентов: {e.Error.Reason}");
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