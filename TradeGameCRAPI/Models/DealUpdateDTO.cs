using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class DealUpdateDTO : DealCreateDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
