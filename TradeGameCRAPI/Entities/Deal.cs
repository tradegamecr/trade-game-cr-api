using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Enums;

namespace TradeGameCRAPI.Entities
{
    public class Deal : BaseEntity
    {
        [Required]
        public User Bidder { get; set; }

        [Required]
        public User Retailer { get; set; }

        public string Message { get; set; }

        [Required]
        public DealStatus Status { get; set; }

        [Required]
        public List<Product> Products { get; set; }
    }
}
