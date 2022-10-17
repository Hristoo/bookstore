using BookStore.Models.Models;

namespace BookStode.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> SavePurchase(Purchase purchase);
        
        Task<Guid> DeletePurchase(Purchase purchase);

        Task<IEnumerable<Purchase>> GetPurchases(int userId);
    }
}
