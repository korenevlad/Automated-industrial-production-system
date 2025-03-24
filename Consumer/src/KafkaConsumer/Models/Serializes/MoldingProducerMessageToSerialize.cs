using System.Text.Json.Serialization;

namespace KafkaConsumer.Models.Serializes;
public class MoldingProducerMessageToSerialize
{
    [JsonPropertyName("Время")]
    public DateTime Time { get; set; }
    [JsonPropertyName("Температура")]
    public double Temperature { get; set; }
    [JsonPropertyName("Оставшееся время")]
    public TimeSpan Remaining_process_time { get; set; }
}