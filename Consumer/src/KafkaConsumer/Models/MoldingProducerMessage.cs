namespace KafkaConsumer.Models;

public class MoldingProducerMessage
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public TimeSpan Remaining_process_time { get; set; }
}