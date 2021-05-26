namespace TradeGameCRAPI.Models
{
    public class ESPost : BaseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
