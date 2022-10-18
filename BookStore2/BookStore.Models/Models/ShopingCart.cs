using System.Collections.Generic;

namespace BookStore.Models.Models
{
    public class ShopingCart
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}
