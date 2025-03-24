using System.Text.Json.Serialization;

namespace KafkaConsumer.Models.Serializes;
public class СuttingArrayProducerMessageToSerialize
{
    [JsonPropertyName("Время")]
    public DateTime Time { get; set; }
    [JsonPropertyName("Давление резки")]
    public double Pressure { get; set; }
    [JsonPropertyName("Скорость резки")]
    public double Speed { get; set; }
}