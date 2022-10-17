using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStode.DL.Repositories.MongoRepositories
{
    public class ShopingCartRepository : IShopingCartRepository
    {
        private ShopingCart _shopingCart;
        private readonly MongoClient _mongoClient;
        private IMongoDatabase _database;
        private IMongoCollection<ShopingCart> _purchaseCollection;
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;


        public ShopingCartRepository(IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration)
        {
            _shopingCart = new ShopingCart();
            _mongoDbConfiguration = mongoDbConfiguration;

            _mongoClient = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);

            _database = _mongoClient.GetDatabase("ShopingCart");
            _purchaseCollection = _database.GetCollection<ShopingCart>("ShopingCart");
        }

        public async Task AddToCart(Book book)
        {
            var cartBooks = _shopingCart.Books.ToList();
            cartBooks.Add(book);    
            //await _purchaseCollection.InsertOneAsync(_shopingCart);
        }

        public Task EmptyCart()
        {
            throw new NotImplementedException();
        }

        public Task<Book> FinishPurchase(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetContent(Book book)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromCart(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
