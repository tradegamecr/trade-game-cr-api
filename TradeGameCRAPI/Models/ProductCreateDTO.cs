using System.ComponentModel.DataAnnotations;
using TradeGameCRAPI.Enums;

namespace TradeGameCRAPI.Models
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int State { get; set; }

        [Required]
        public ProductType Type { get; set; }

        [Required]
        public int UserId { get; set; }

        public string Note { get; set; }

        public string ImageSmall { get; set; }

        public string Image { get; set; }
    }
}
