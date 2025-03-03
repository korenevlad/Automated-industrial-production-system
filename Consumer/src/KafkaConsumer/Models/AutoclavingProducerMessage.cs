namespace KafkaConsumer.Models;

public class AutoclavingProducerMessage
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public double Pressure { get; set; }
    public TimeSpan Remaining_process_time { get; set; }
}