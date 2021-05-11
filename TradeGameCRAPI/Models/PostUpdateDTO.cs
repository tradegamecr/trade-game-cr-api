using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class PostUpdateDTO : PostCreateDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
