using BookStore.Models.Models;

namespace BookStode.DL.Interfaces
{
    public interface IShopingCartRepository
    {
        Task<IEnumerable<ShopingCart>> GetContent(int userId);

        Task AddToCart(Book book);

        Task RemoveFromCart(Book book);

        Task EmptyCart();

        Task<Book> FinishPurchase(Book book);
    }
}
