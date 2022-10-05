namespace BookStore.Models.Models.Requests
{
    public class AddBookRequest
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; }

        public decimal Price { get; set; }
    }
}
