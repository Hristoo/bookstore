using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace BookStore.BL.Kafka
{
    public class Consumer<TKey, Tvalue> : IHostedService
    {
        private readonly IOptionsMonitor<KafkaSettings> _settings;
        private readonly IConsumer<TKey, Tvalue> _consumer;

        public Consumer(IOptionsMonitor<KafkaSettings> settings)
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
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    Console.WriteLine($"Consumer: {cr.Message.Key} value {cr.Message.Value}");

                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
