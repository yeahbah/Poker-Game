namespace Server.Models
{
    public class PayScheduleModel
    {
        public decimal Pair { get; set; }
        public decimal TwoPair { get; set; }
        public decimal ThreeofAkind { get; set; }
        public decimal Straight { get; set; }
        public decimal Flush { get; set; }
        public decimal Fullhouse { get; set; }
        public decimal FourOfAKind { get; set; }
        public decimal StraightFlush { get; set; }
        public decimal RoyalFlush { get; set; }
    }
}