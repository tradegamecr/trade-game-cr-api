using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Enums;

namespace TradeGameCRAPI.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string ImageSmall { get; set; }

        public string Image { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public int State { get; set; }

        [Required]
        public ProductType Type { get; set; }

        public string Note { get; set; }

        [Required]
        public User Owner { get; set; }
    }
}
