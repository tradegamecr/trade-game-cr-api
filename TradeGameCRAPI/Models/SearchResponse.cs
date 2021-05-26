using System.Collections.Generic;

namespace TradeGameCRAPI.Models
{
    public class SearchResponse
    {
        public SearchResponse(SearchPageInfo searchPageInfo, List<CardDTO> data)
        {
            SearchPageInfo = searchPageInfo;
            Data = data;
        }

        public SearchPageInfo SearchPageInfo { get; set; }

        public List<CardDTO> Data { get; set; }
    }
}
