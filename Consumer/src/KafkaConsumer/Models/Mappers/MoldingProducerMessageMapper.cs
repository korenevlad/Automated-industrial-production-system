using KafkaConsumer.Models.Serializes;

namespace KafkaConsumer.Models.Mappers;
public static class MoldingProducerMessageMapper
{
    public static MoldingProducerMessageToSerialize Map(MoldingProducerMessage source)
    {
        return new MoldingProducerMessageToSerialize
        {
            Time = source.Time,
            Temperature = source.Temperature,
            Remaining_process_time = source.Remaining_process_time
        };
    }
}