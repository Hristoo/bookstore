using Confluent.Kafka;
using MessagePack;

namespace BookStore.BL.Kafka
{
    public class CustomPackSerialize<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}
