using System.Threading.Tasks.Dataflow;
using Confluent.Kafka;
using DataflowAPI.Kafka;
using DataflowAPI.Models;
using DataflowAPI.Models.Configuration;
using MessagePack;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using static System.Reflection.Metadata.BlobBuilder;

namespace DataflowAPI.Services
{
    public class GeneratePurchaseService : IHostedService
    {
        private Purchase _purchase;
        private readonly Producer<Guid, Purchase> _producer;
        private TransformBlock<Purchase, Purchase> _generatePurchase;
        private ActionBlock<Purchase> _sendMessage;

        public GeneratePurchaseService(Producer<Guid, Purchase> producer)
        {
            _producer = producer;
            _purchase = new Purchase();
            _generatePurchase = new TransformBlock<Purchase, Purchase>(result =>
            {
                result.Id = Guid.NewGuid();
                result.Books = GetRandomBook();
                result.TotalMoney = 10;
                result.UserId = 5;             
                result.AdditionalInfo = new List<string>() { "test", "test2" };

                return result;
            });

            _sendMessage = new ActionBlock<Purchase>(async value =>
            {
                var msg = new Message<Guid, Purchase>()
                {
                    Key = value.Id,
                    Value = value
                };

                await _producer.SendMessage(msg);
            });
            _generatePurchase.LinkTo(_sendMessage);
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(async () =>
              {
                  while (!cancellationToken.IsCancellationRequested)
                  {
                      _generatePurchase.Post(_purchase);

                      await Task.Delay(IntervalSettings.PurchaseInterval);
                  }
              });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Purchase> GeneratePurchase()
        {
            _purchase.Books = GetRandomBook();

            return _purchase;
        }

        private IEnumerable<Book> GetRandomBook()
        {
            var rand = new Random();
            var books = new List<Book>();

            for (int i = 0; i < rand.Next(1, 3); i++)
            {
                books.Add(new Book()
                {
                    Title = $"title{rand.Next(1, 10)}",
                    Quantity = rand.Next(1, 10),
                    AuthorId = rand.Next(1, 10),
                    Price = rand.Next(20, 50),
                    Id = 5,
                    LastUpdated = DateTime.Now
                });
            }

            return books;
        }
    }
}
