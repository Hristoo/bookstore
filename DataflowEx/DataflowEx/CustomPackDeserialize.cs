using Confluent.Kafka;
using MessagePack;

namespace DataflowEx
{
    public class CustomPackDeserialize<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return MessagePackSerializer.Deserialize<T>(data.ToArray());
        }
    }
}
