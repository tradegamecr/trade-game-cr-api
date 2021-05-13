using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeGameCRAPI.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int SuccessfulDeals { get; set; }

        public string City { get; set; }

        public int Phone { get; set; }

        // Navigation

        public List<Product> Products { get; set; }

        public List<Post> Posts { get; set; }

        [InverseProperty("Retailer")]
        public List<Deal> Retails { get; set; } = new List<Deal>();

        [InverseProperty("Bidder")]
        public List<Deal> Bids { get; set; } = new List<Deal>();
    }
}
