using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Models
{
    public class UpdateUserInput : IUpdateInput
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public int? SuccessfulDeals { get; set; } = 0;

        public string? Picture { get; set; }

        public string? City { get; set; }

        public int? Phone { get; set; }
    }
}
