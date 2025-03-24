using KafkaConsumer.Models.Serializes;

namespace KafkaConsumer.Models.Mappers;

public static class AutoclavingProducerMessageMapper
{
    public static AutoclavingProducerMessageToSerialiize Map(AutoclavingProducerMessage source)
    {
        return new AutoclavingProducerMessageToSerialiize
        {
            Time = source.Time,
            Temperature = source.Temperature,
            Pressure = source.Pressure,
            Remaining_process_time = source.Remaining_process_time
        };
    }
}