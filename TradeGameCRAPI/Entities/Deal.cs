using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TradeGameCRAPI.Enums;

namespace TradeGameCRAPI.Entities
{
    public class Deal : BaseEntity
    {
        [Required]
        public DealStatus Status { get; set; }

        [Required]
        public string Message { get; set; }

        // Navigation

        public List<Product> Products { get; set; }

        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public User Retailer { get; set; }

        [ForeignKey("Bidder")]
        public int? BidderId { get; set; }
        public User? Bidder { get; set; }
    }
}
