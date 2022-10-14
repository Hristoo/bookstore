using MessagePack;

namespace CacheAPI.Models
{
    [MessagePackObject]
    public class Book
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int AuthorId { get; set; }
        [Key(2)]
        public string Title { get; set; }
        [Key(3)]
        public int Quantity { get; set; }
        [Key(4)]
        public DateTime LastUpdated { get; set; }
        [Key(5)]
        public decimal Price { get; set; }


    }
}
