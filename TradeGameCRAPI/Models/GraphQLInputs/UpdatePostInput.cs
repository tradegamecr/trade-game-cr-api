using System.Collections.Generic;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Models
{
    public class UpdatePostInput : IUpdateInput
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }

        public List<int>? ProductsId { get; set; }
    }
}
