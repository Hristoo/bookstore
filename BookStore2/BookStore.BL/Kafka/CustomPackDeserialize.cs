using Confluent.Kafka;
using MessagePack;

namespace BookStore.BL.Kafka
{
    public class CustomPackDeserialize<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return MessagePackSerializer.Deserialize<T>(data.ToArray());
        }
    }
}
