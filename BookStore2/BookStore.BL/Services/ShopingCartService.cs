using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class ShopingCartService : IShopingCartService
    {
        private readonly IShopingCartRepository _shopingCartRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly Purchase _purchase;
        private List<Book> _books;

        public ShopingCartService(IShopingCartRepository shopingCartRepository, IPurchaseRepository purchaseRepository)
        {
            _shopingCartRepository = shopingCartRepository;
            _books = new List<Book>();
            _purchase = new Purchase()
            {
                Books = _books,
                UserId = 111,
                Id = new Guid()

            };
            _purchaseRepository = purchaseRepository;
        }

        public Task AddToCart(Book book)
        {
            _shopingCartRepository.AddToCart(book);
            return Task.CompletedTask;
        }

        public Task EmptyCart()
        {
            _books.Clear();
            return Task.CompletedTask;
        }

        public Task DeletePurchase(Purchase purchase)
        {
            _purchaseRepository.DeletePurchase(purchase);
            return Task.CompletedTask;
        }

        public Task FinishPurchase()
        {         
            _purchaseRepository.SavePurchase(_purchase);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Purchase>> GetContent(int userId)
        {
            return  _purchaseRepository.GetPurchases(userId);
        }

        public Book RemoveFromCart(int bookId)
        {
            var bookToRemove = _books.FirstOrDefault(x => x.Id == bookId);

            if (bookToRemove != null)
            {
                _books.Remove(bookToRemove);    
            }

            return bookToRemove;
        }
    }
}
