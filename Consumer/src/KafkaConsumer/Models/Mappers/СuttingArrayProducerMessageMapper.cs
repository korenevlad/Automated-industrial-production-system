using KafkaConsumer.Models.Serializes;

namespace KafkaConsumer.Models.Mappers;
public static class СuttingArrayProducerMessageMapper
{
    public static СuttingArrayProducerMessageToSerialize Map(СuttingArrayProducerMessage source)
    {
        return new СuttingArrayProducerMessageToSerialize
        {
            Time = source.Time,
            Pressure = source.Pressure,
            Speed = source.Speed
        };
    }
}