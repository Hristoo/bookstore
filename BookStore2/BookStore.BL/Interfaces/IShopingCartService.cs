using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IShopingCartService
    {
        Task<IEnumerable<Purchase>> GetContent(int userId);

        Task AddToCart(Book book);

        Book RemoveFromCart(int bookId);

        Task EmptyCart();

        Task DeletePurchase(Purchase purchase);

        Task FinishPurchase();
    }
}
