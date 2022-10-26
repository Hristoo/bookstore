using System.Threading.Tasks.Dataflow;
using BookStode.DL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.Models.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace BookStore.BL.Services
{
    public class DataflowService<TKey, TValue> : IHostedService
    {
        private readonly IConsumer<TKey, Purchase> _consumer;
        private readonly IBookRepository _bookRepository;
        public DataflowService(IBookRepository bookRepository)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = "24",
            };

            _consumer = new ConsumerBuilder<TKey, Purchase>(config)
                .SetKeyDeserializer(new CustomPackDeserialize<TKey>())
                .SetValueDeserializer(new CustomPackDeserialize<Purchase>())
                .Build();

            _consumer.Subscribe($"Dataflow");
            _bookRepository = bookRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri("https://localhost:7057/AditionalAuthorInfo/GetAll")
                };

                while (!cancellationToken.IsCancellationRequested)
                {
                    var cr = _consumer.Consume();


                    var transformBlock = new TransformBlock<Purchase, Purchase>(async result =>
                    {
                        var books = result.Books;

                        var test = await client.GetAsync(client.BaseAddress); // ???/

                        Console.WriteLine(test);

                        return result;
                    });

                    var actionBlock = new ActionBlock<Purchase>(value =>
                    {
                        Console.WriteLine($"done: {value.Books.First()}");
                    });

                    transformBlock.LinkTo(actionBlock);

                    transformBlock.Post(cr.Message.Value);
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
