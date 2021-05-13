using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Product> Products { get; set; }
    }
}
