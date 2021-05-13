using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class UserCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int SuccessfulDeals { get; set; } = 0;

        public string City { get; set; }

        public int Phone { get; set; }
    }
}
