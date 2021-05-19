using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class UserDTO : BaseDTO
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int SuccessfulDeals { get; set; } = 0;

        public string? City { get; set; }

        public int? Phone { get; set; }

        /*public List<DealDTO>? Retails { get; set; } = new List<DealDTO>();

        public List<DealDTO>? Bids { get; set; } = new List<DealDTO>();*/
    }
}
