using System.Text.Json.Serialization;

namespace KafkaConsumer.Models.Serializes;
public class AutoclavingProducerMessageToSerialiize
{
    [JsonPropertyName("Время")]
    public DateTime Time { get; set; }
    [JsonPropertyName("Температура")]
    public double Temperature { get; set; }
    [JsonPropertyName("Давление")]
    public double Pressure { get; set; }
    [JsonPropertyName("Оставшееся время")]
    public TimeSpan Remaining_process_time { get; set; }
}