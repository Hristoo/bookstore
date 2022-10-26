using MessagePack;

namespace DataflowEx
{
    [MessagePackObject]
    public class Book
    {
        [Key(0)]
        public string Title { get; set; }
        [Key(1)]
        public int Quantity { get; set; }
        [Key(2)]
        public int AuthorId { get; set; }
        [Key(3)]
        public int Price { get; set; }
    }
}
