using System.Text.Json;
using System;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Options;
using Confluent.Kafka;
using System.Threading.Tasks.Dataflow;

namespace BookStode.DL.Repositories.MongoRepositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;
        private readonly MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<Purchase> _purchaseCollection;

        public PurchaseRepository(IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration;
            //_mongoClient = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            //_database = _mongoClient.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);
            //_purchaseCollection = _database.GetCollection<Purchase>(_mongoDbConfiguration.CurrentValue.PurchaseDatabase);
        }

        public async Task<Guid> DeletePurchase(Purchase purchase)
        {
            var purchaseDeleteResult = await _purchaseCollection.DeleteOneAsync(x => x.Id == purchase.Id);

            return purchase.Id;
        }

        public async Task<IEnumerable<Purchase>> GetPurchases(int userId)
        {
            return (await _purchaseCollection.FindAsync(x => x.UserId == userId)).ToList();
        }

        public async Task<Purchase> SavePurchase(Purchase purchase)
        {
          
            var transformBlock = new TransformBlock<Purchase, Purchase>(result =>
            {
                var books = result.Books;
                foreach (var book in books)
                {
                    Console.WriteLine(book.Quantity ); 
                }

                return result;
            });

            var actionBlock = new ActionBlock<Purchase>(value =>
            {
                Console.WriteLine($"{value}");
            });


            transformBlock.LinkTo(actionBlock);

            await _purchaseCollection.InsertOneAsync(purchase);


            return purchase;
        }
    }
}
