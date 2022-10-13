using System;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Confluent.Kafka;
using MessagePack;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace BookStore.BL.Kafka
{
    public class Producer<TKey, TValue>
    {
        private readonly IOptionsMonitor<KafkaSettings> _settings;
        private readonly IProducer<TKey, TValue> _producer;

        public Producer(IOptionsMonitor<KafkaSettings> settings)
        {
            _settings = settings;

            var config = new ProducerConfig()
            {
                BootstrapServers = _settings.CurrentValue.BootstrapServers,
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
                var result = await _producer.ProduceAsync("personTopic", msg);

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
