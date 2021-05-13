using System.ComponentModel.DataAnnotations;
using TradeGameCRAPI.Enums;

namespace TradeGameCRAPI.Models
{
    public class DealCreateDTO
    {
        [Required]
        public DealStatus Status { get; set; }

        [Required]
        public int RetailerId { get; set; }

        [Required]
        public int BidderId { get; set; }

        public string Message { get; set; }
    }
}
