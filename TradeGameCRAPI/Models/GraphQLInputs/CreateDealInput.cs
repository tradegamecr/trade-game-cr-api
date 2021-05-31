using TradeGameCRAPI.Enums;

namespace TradeGameCRAPI.Models
{
    public class CreateDealInput
    {
        public DealStatus Status { get; set; }

        public int RetailerId { get; set; }

        public int? BidderId { get; set; }

        public string? Message { get; set; }
    }
}
