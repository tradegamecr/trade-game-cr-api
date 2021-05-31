using System;

namespace TradeGameCRAPI.Models
{
    public class CardDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lang { get; set; }
        public ImageUrisDTO ImageUris { get; set; }
        public string? TypeLine { get; set; }
        public string? OracleText { get; set; }
        public string? FlavorText { get; set; }
        public RelatedUrisDTO RelatedUris { get; set; }
    }

    public class ImageUrisDTO
    {
        public Uri Small { get; set; }
        public Uri Normal { get; set; }
        public Uri Large { get; set; }
        public Uri Png { get; set; }
        public Uri ArtCrop { get; set; }
        public Uri BorderCrop { get; set; }
    }

    public class RelatedUrisDTO
    {
        public Uri Gatherer { get; set; }
        public Uri TcgplayerInfiniteArticles { get; set; }
        public Uri TcgplayerInfiniteDecks { get; set; }
        public Uri Edhrec { get; set; }
        public Uri Mtgtop8 { get; set; }
    }
}
