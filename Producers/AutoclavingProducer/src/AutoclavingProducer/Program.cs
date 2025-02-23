using System.Text.Json;
using Confluent.Kafka;

public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ProducerConfig { BootstrapServers = "kafka:9093" };
        string topic = "autoclaving_producer";

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            Random rand = new Random();

            while (true)
            {
                var sensorData = new
                {
                    Speed = rand.Next(200, 400),
                    Temperature = rand.Next(40, 60),
                    Pressure = rand.Next(10, 16)
                };

                string message = JsonSerializer.Serialize(sensorData);

                try
                {
                    var deliveryResult = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Message delivered to {deliveryResult.TopicPartitionOffset}: {message}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Error producing message: {e.Error.Reason}");
                }

                await Task.Delay(1000);
            }
        }
    }
}