using Confluent.Kafka;

namespace DataflowEx
{
    public class Consumer<TKey, TValue>
    {
        private readonly IConsumer<TKey, byte[]> _consumer;

        public Consumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = "10",
            };

            _consumer = new ConsumerBuilder<TKey, byte[]>(config)
                .Build();

            _consumer.Subscribe($"Dataflow");
        }

        public byte[] GetConsumer()
        {
            return Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();
                    _dataflowService.ChangeQty();
                    _cache._dictionary.Add(cr.Message.Key, cr.Message.Value);
                }
            });

            return Task.CompletedTask;
        }
    }
}