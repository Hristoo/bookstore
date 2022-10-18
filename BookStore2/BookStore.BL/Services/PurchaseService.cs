using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private List<Purchase> _purchaseList;
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(List<Purchase> purchaseList, IPurchaseRepository purchaseRepository)
        {
            _purchaseList = new List<Purchase>();
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Guid> DeletePurchase(Purchase purchase)
        {
            return await _purchaseRepository.DeletePurchase(purchase);
        }

        public async Task<IEnumerable<Purchase>> GetPurchases(int userId)
        {
            return await _purchaseRepository.GetPurchases(userId);
        }

        public async Task<Purchase?> SavePurchase(Purchase purchase)
        {
           return await _purchaseRepository.SavePurchase(purchase);
        }
    }
}
