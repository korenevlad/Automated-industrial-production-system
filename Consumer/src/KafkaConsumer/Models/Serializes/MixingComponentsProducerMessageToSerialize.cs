using System.Text.Json.Serialization;

namespace KafkaConsumer.Models.Serializes;
public class MixingComponentsProducerMessageToSerialize
{
    [JsonPropertyName("Время")]
    public DateTime Time { get; set; }
    [JsonPropertyName("Температура смеси")]
    public double Temperature_mixture { get; set; }
    [JsonPropertyName("Скорость смешивания")]
    public double Mixing_speed { get; set; }
    [JsonPropertyName("Оставшееся время")]
    public TimeSpan Remaining_process_time { get; set; }
}