using AutoMapper;
using Nest;
using System;
using System.Collections.Generic;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Helpers
{
    public class SearchResponseBuilder
    {
        private readonly ISearchResponse<ESCard> searchResponse;
        private readonly string query;
        private readonly int from;
        private readonly int size;
        private readonly IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ESCard, CardDTO>();
            cfg.CreateMap<ESImageUris, ImageUrisDTO>();
            cfg.CreateMap<ESRelatedUris, RelatedUrisDTO>();
        }).CreateMapper();

        public SearchResponseBuilder
            (ISearchResponse<ESCard> searchResponse, string query, int from, int size)
        {
            this.searchResponse = searchResponse;
            this.query = query;
            this.from = from;
            this.size = size;
        }

        public SearchPageInfo GetPageInfo()
        {
            var total = (int)searchResponse.HitsMetadata.Total.Value;
            var pagesTotal = Convert.ToInt32(Math.Ceiling((decimal)(total / size)));
            var currentPage = from / size;
            var hasNextPage = pagesTotal < currentPage;
            var searchPageInfo = new SearchPageInfo()
            {
                Total = total,
                PagesTotal = pagesTotal,
                CurrentPage = currentPage,
                HasNextPage = hasNextPage,
                Query = query
            };

            return searchPageInfo;
        }

        public List<CardDTO> GetData()
        {
            var data = new List<CardDTO>();
            var hitsEnumerator = searchResponse.Hits.GetEnumerator();

            while (hitsEnumerator.MoveNext())
            {
                data.Add(mapper.Map<CardDTO>(hitsEnumerator.Current.Source));
            }

            return data;
        }

        public SearchResponse GetResponse()
        {
            var pageInfo = GetPageInfo();
            var data = GetData();

            return new SearchResponse(pageInfo, data);
        }
    }
}
