using Web.Services;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class NewGameModel
    {
        [Required]
        [Range(1, 5000)]
        public decimal PlayMoney { get; set; }

        [Required]
        [Range(0.05, 1)]
        public decimal UnitSize { get; set; }

        [Required]
        public VideoPokerType VideoPokerType { get; set; }
    }
}