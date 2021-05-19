using TradeGameCRAPI.Enums;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Models
{
    public class UpdateProductInput : IUpdateInput
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int State { get; set; }

        public ProductType Type { get; set; }

        public int UserId { get; set; }

        public string Note { get; set; }

        public string ImageSmall { get; set; }

        public string Image { get; set; }
    }
}
