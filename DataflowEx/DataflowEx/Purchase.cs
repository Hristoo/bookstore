using MessagePack;

namespace DataflowEx
{
    [MessagePackObject]
    public record Purchase
    {
        [Key(0)]
        public Guid Id { get; set; }
        [Key(1)]
        public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
        [Key(2)]
        public decimal TotalMoney { get; set; }
        [Key(3)]
        public int UserId { get; set; }
        [Key(4)]
        public IEnumerable<string> AdditionalInfo { get; set; } = Enumerable.Empty<string>();
    }
}
