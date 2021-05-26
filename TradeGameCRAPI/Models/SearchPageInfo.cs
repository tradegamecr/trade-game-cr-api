using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Models
{
    public class SearchPageInfo
    {
        public int Total { get; set; }

        public int PagesTotal { get; set; }

        public int CurrentPage { get; set; }

        public bool HasNextPage { get; set; }

        public string Query { get; set; }
    }
}
