using System.ComponentModel.DataAnnotations;
using Web.Services;

namespace Web.Models
{
    public class GameVarsModel
    {
        [Required]
        public VideoPokerType VideoPokerType { get; set; }

        [Required]
        [Range(0, 1)]
        public decimal UnitSize { get; set; }

        [Required]
        [Range(1, 25)]
        public int BetSize { get; set; }

        [Required]
        [Range(1, 5000)]
        public decimal Money { get; set; }
    }
}
