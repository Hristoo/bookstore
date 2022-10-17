using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopingCartController : ControllerBase
    {
       private readonly IShopingCartService _shopingCartService;

        public ShopingCartController(IShopingCartService shopingCartService)
        {
            _shopingCartService = shopingCartService;
        }

        [HttpPost(nameof(AddToCart))]
        public async Task<IActionResult> AddToCart(Book book)
        {
            return Ok(_shopingCartService.AddToCart(book));
        }

        [HttpGet(nameof(GetContent))]
        public Task<IEnumerable<Purchase>> GetContent(int userId)
        {
            return _shopingCartService.GetContent(userId);
        }

        [HttpGet(nameof(RemoveFromCart))]
        public Book RemoveFromCart(int bookId)
        {
            return _shopingCartService.RemoveFromCart(bookId);
        }

        [HttpPost(nameof(EmptyCart))]
        public async Task<IActionResult> EmptyCart()
        {
            return Ok(_shopingCartService.EmptyCart());
        }

        [HttpPost(nameof(DeletePurchase))]
        public async Task<IActionResult> DeletePurchase(Purchase purchase)
        {
            return Ok(_shopingCartService.DeletePurchase(purchase));
        }

        [HttpPost(nameof(FinishPurchase))]
        public Task FinishPurchase()
        {
            return _shopingCartService.FinishPurchase();
        }
    }
}
