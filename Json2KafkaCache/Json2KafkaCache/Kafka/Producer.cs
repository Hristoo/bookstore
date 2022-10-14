using Confluent.Kafka;
using Json2KafkaCache.Models;

namespace BookStore.BL.Kafka
{
    public class Producer<TKey, TValue> where TValue: ICacheItem<TKey>
    {
        private readonly IProducer<TKey, TValue> _producer;

        public Producer()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "Localhost:9092",
            };

            _producer = new ProducerBuilder<TKey, TValue>(config)
                .SetKeySerializer(new CustomPackSerialize<TKey>())
                .SetValueSerializer(new CustomPackSerialize<TValue>())
                .Build();
        }

        public async Task SendMessage(Message<TKey, TValue> msg)
        {
            try
            {             
                var result = await _producer.ProduceAsync(typeof(TValue).Name, msg);

                if (result != null)
                {
                    Console.WriteLine($"Delivered: {result.Value} to {result.TopicPartitionOffset}");
                }

            }
            catch (ProduceException<TKey, TValue> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }

    }
}
