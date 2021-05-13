using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class UserDTO : BaseDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int SuccessfulDeals { get; set; }

        public string City { get; set; }

        public int Phone { get; set; }
    }
}
