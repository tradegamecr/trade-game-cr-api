using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class ProductUpdateDTO : ProductCreateDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
