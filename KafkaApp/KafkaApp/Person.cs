using MessagePack;

namespace KafkaProducer
{
    [MessagePackObject]
    public record Person
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
        [Key(2)]
        public int Age { get; set; }
    }
}
