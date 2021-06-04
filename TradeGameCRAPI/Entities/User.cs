using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeGameCRAPI.Entities
{
    public class User : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [Required]
        public int SuccessfulDeals { get; set; } = 0;

        public string? City { get; set; }

        public int? Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation

        public List<Product>? Products { get; set; }

        public List<Post>? Posts { get; set; }

        [InverseProperty("Retailer")]
        public List<Deal>? Retails { get; set; } = new List<Deal>();

        [InverseProperty("Bidder")]
        public List<Deal>? Bids { get; set; } = new List<Deal>();
    }
}
