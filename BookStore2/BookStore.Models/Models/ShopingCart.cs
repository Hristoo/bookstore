using System.Collections.Generic;

namespace BookStore.Models.Models
{
    public class ShopingCart
    {
        public int Id { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public int Total { get; set; }  

        public int UserId { get; set; }
    }
}
