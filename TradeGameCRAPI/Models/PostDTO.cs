using System.Collections.Generic;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Models
{
    public class PostDTO : BaseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }

        public List<Product> Products { get; set; }
    }
}
