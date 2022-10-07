using BookStore.Models.Models;

namespace BookStore.Test
{
    public class BookTest
    {
        private List<Author> _books = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                AuthorId = 55,
                Title = "Book title",
                Price = 10,
                Quantity = 5,
                LastUpdated = DateTime.Now,
            },
            new Author()
            {
                Id = 2,
                AuthorId = 5,
                Title = "Book title2",
                Price = 10,
                Quantity = 5,
                LastUpdated = DateTime.Now,
            }
        };
    }
}
