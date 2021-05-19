using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Models
{
    public class CreatePostInput
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; } = true;

        public int UserId { get; set; }
    }
}
