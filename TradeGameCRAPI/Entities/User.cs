using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(0)]
        public int SuccessfulDeals { get; set; }
    }
}
