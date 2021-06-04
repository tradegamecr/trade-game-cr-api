using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class CreateUserInput
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? Picture { get; set; }

        public int? SuccessfulDeals { get; set; } = 0;

        public string? City { get; set; }

        public int? Phone { get; set; }
    }
}
