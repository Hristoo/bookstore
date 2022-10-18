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
        private IMongoCollection<ShopingCart> _shopingCartCollection;
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoDbConfiguration;


        public ShopingCartRepository(IOptionsMonitor<MongoDbConfiguration> mongoDbConfiguration)
        {
            _shopingCart = new ShopingCart();
            _mongoDbConfiguration = mongoDbConfiguration;
            _mongoClient = new MongoClient(_mongoDbConfiguration.CurrentValue.ConnectionString);
            _database = _mongoClient.GetDatabase(_mongoDbConfiguration.CurrentValue.DatabaseName);
            _shopingCartCollection = _database.GetCollection<ShopingCart>(_mongoDbConfiguration.CurrentValue.ShopingCartDatabase);
        }

        public async Task AddToCart(Book book)
        {
            var cartBooks = _shopingCart.Books.ToList();
            cartBooks.Add(book);
            await _shopingCartCollection.InsertOneAsync(_shopingCart);
        }

        public Task EmptyCart()
        {
            throw new NotImplementedException();
        }

        public Task<Book> FinishPurchase(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShopingCart>> GetContent(int userId)
        {

            return (await _shopingCartCollection.FindAsync(x => x.UserId == userId)).ToList();
        }

        public Task RemoveFromCart(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
