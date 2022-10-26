using BookStore.BL.Services;
using BookStore.Caches;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class Consumer<TKey, TValue> : IHostedService
    {
        private readonly IOptionsMonitor<KafkaSettings> _settings;
        private readonly IConsumer<TKey, TValue> _consumer;
        public Subcribe2Cache<TKey, TValue> _cache;

        public Consumer(IOptionsMonitor<KafkaSettings> settings, Subcribe2Cache<TKey, TValue> cache)
        {
            _settings = settings;

            var config = new ConsumerConfig
            {
                BootstrapServers = _settings.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _settings.CurrentValue.GroupId,
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config)
                .SetKeyDeserializer(new CustomPackDeserialize<TKey>())
                .SetValueDeserializer(new CustomPackDeserialize<TValue>())
                .Build();

            _consumer.Subscribe($"{_settings.CurrentValue.Topic}");
            _cache = cache;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {              
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    _cache._dictionary.Add(cr.Message.Key, cr.Message.Value);
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Dispose();
            return Task.CompletedTask;
        }
    }
}
