namespace KafkaConsumer.Models;

public class MixingComponentsProducerMessage
{
    public DateTime Time { get; set; }
    public double Temperature_mixture { get; set; }
    public double Mixing_speed { get; set; }
    public TimeSpan Remaining_process_time { get; set; }
}