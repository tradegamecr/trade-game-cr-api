using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
