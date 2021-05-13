using System.ComponentModel.DataAnnotations;
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
        [Range(1, 5)]
        public int State { get; set; }

        [Required]
        public ProductType Type { get; set; }

        public string Note { get; set; }

        // Navigation

        public int UserId { get; set; }
        public User User { get; set; }

        public int? PostId { get; set; }
        public Post Post { get; set; }

        public int? DealId { get; set; }
        public Deal Deal { get; set; }
    }
}
