using MessagePack;

namespace Json2KafkaCache.Models
{
    [MessagePackObject]
    public class BranchInfo : ICacheItem<int>
    {
        [Key(0)]
        public int BranchID { get; set; }
        [Key(1)]
        public string Name { get; set; }
        [Key(2)]
        public int OrderId { get; set; }
        [Key(3)]
        public bool IsActive { get; set; }
        [Key(4)]
        public bool IsDrawCapable { get; set; }
        [Key(5)]
        public string ENetPulsBranchID { get; set; }
        [Key(6)]
        public int LiveMasterLeagueID { get; set; }
        [Key(7)]
        public bool IsRacing { get; set; }
        [Key(8)]
        public bool IsVirtualSport { get; set; }
        [Key(9)]
        public bool AllowComboFromTheSameEvent { get; set; }
        [Key(10)]
        public bool IsFixPO { get; set; }
        [Key(11)]
        public int NumberOfParts { get; set; }
        [Key(12)]
        public int SecondsInOnePart { get; set; }

        public int GetKey() => BranchID;
    }
}