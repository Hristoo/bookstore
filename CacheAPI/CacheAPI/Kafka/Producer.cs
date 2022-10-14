using BookStore.Models.Models.Configurations;
using CacheAPI.Repositories;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace BookStore.BL.Kafka
{
    public class Producer<TKey, TValue>
    {
        private readonly IOptionsMonitor<KafkaSettings> _settings;
        private readonly IProducer<TKey, TValue> _producer;
        private readonly Repository<TKey, TValue> _repo;


        public Producer(IOptionsMonitor<KafkaSettings> settings, Repository<TKey, TValue> repo)
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
            _repo = repo;
        }

        public async Task Produce( TKey key, TValue item)
        {
            var msg = new Message<TKey, TValue>()
            {
                Key = key,
                Value = item
            };

            await _producer.ProduceAsync($"{_settings.CurrentValue.Topic}.{typeof(TValue).Name}", msg);
        }
    }
}
