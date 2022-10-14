using BookStore.Models.Models.Configurations;
using CacheAPI.Models;
using CacheAPI.Repositories;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookStore.BL.Kafka
{
    public class Consumer<TKey, Tvalue> : IHostedService
    {
        private readonly IOptionsMonitor<KafkaSettings> _settings;
        private readonly IConsumer<TKey, Tvalue> _consumer;
        private readonly Repository<int, Book> _repo;

        public Consumer(IOptionsMonitor<KafkaSettings> settings, Repository<int, Book> repo)
        {
            _settings = settings;

            var config = new ConsumerConfig
            {
                BootstrapServers = _settings.CurrentValue.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = _settings.CurrentValue.GroupId,
            };

            _consumer = new ConsumerBuilder<TKey, Tvalue>(config)
                .SetKeyDeserializer(new CustomPackDeserialize<TKey>())
                .SetValueDeserializer(new CustomPackDeserialize<Tvalue>())
                .Build();

            _consumer.Subscribe(_settings.CurrentValue.Topic);
            _repo = repo;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                Console.WriteLine($"Consumer");
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
