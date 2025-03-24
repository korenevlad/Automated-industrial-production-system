using KafkaConsumer.Models.Serializes;

namespace KafkaConsumer.Models.Mappers;
public static class MixingComponentsProducerMessageMapper
{
    public static MixingComponentsProducerMessageToSerialize Map(MixingComponentsProducerMessage source)
    {
        return new MixingComponentsProducerMessageToSerialize
        {
            Time = source.Time,
            Temperature_mixture = source.Temperature_mixture,
            Mixing_speed = source.Mixing_speed,
            Remaining_process_time = source.Remaining_process_time
        };
    }
}