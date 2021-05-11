using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Models
{
    public class PostDTO : BaseDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public List<Product> Products { get; set; }

        [Required]
        public User User { get; set; }
    }
}
