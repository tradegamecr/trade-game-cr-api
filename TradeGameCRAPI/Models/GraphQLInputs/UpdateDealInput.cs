using TradeGameCRAPI.Enums;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Models
{
    public class UpdateDealInput : IUpdateInput
    {
        public int Id { get; set; }

        public DealStatus Status { get; set; }

        public int RetailerId { get; set; }

        public int? BidderId { get; set; }

        public string Message { get; set; }
    }
}
